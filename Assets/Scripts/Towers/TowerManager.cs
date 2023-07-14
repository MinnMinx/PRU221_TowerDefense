using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(50)]
public class TowerManager : MonoBehaviour
{
    [SerializeField]
    private Image previewImg;
    [SerializeField]
    private Canvas parentCanvas;
    [SerializeField]
    private MapController mapController;
    [SerializeField]
    private GameObject upgradeGroup;
    [SerializeField]
    private Button btnChecked;
    [SerializeField]
    private Button btnCancel;
    [SerializeField]
    private SlotChooserManager slotChooser;


    private Dictionary<Vector3Int, Tower> placedTower;
    private Vector3Int lastSelectCell;
    private Vector3Int? levelingUpTowerTile;
    private bool isUpgrading = false;
    public bool IsTowerUpgrading => isUpgrading;
    private bool isRemoving;
    public bool IsRemovingTower => isRemoving;

    public static string SPAWN_TOWER_EVT = "SPAWN_TOWER_EVT";
    public static string SAVE_TOWER_EVT = "SAVE_TOWER_EVT";
    public static string LOAD_TOWER_EVT = "LOAD_TOWER_EVT";
    public static string PLAYERPREF_SAVEDATA = "saved_tower";

    // Start is called before the first frame update
    void Start()
    {
        placedTower = new Dictionary<Vector3Int, Tower>();
        GameUiEventManager.Instance.RegisterEvent(SPAWN_TOWER_EVT, SpawnTower);
        GameUiEventManager.Instance.RegisterEvent(SAVE_TOWER_EVT, SaveTower);
        GameUiEventManager.Instance.RegisterEvent(LOAD_TOWER_EVT, LoadTower);
        GameUiEventManager.Instance.RegisterEvent(SlotData.SLOT_CLICK_EVT, delegate
        {
            DisableRemoving();
        });
        DisableRemoving();
        levelingUpTowerTile = null;

        // check if user click button checked to upgrade tower
        btnChecked.onClick.AddListener(() =>
        {
            if (levelingUpTowerTile.HasValue && placedTower.TryGetValue(levelingUpTowerTile.Value, out var tower))
                tower.UpgradeTower();
            FinishLevelingUp();
        });

        // check if user click button cancel to unpause game
        btnCancel.onClick.AddListener(FinishLevelingUp);
    }

    private void Update()
    {
        if (isRemoving)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(parentCanvas.transform as RectTransform,
                    Input.mousePosition, parentCanvas.worldCamera, out Vector3 mousePos))
            {
                previewImg.transform.position = mousePos;
                bool placeable = mapController.IsPlaceableTile(mousePos, out _, out Vector3Int tileCell);
                if (tileCell != lastSelectCell && placedTower.ContainsKey(lastSelectCell))
                {
                    // return last tower color to normal
                    var sprRenderer = placedTower[lastSelectCell].GetComponent<SpriteRenderer>();
                    sprRenderer.color = Color.white;
                    lastSelectCell = tileCell;
                }
                if (placeable)
                    lastSelectCell = tileCell;
                if (placedTower.ContainsKey(tileCell))
                {
                    var sprRenderer = placedTower[tileCell].GetComponent<SpriteRenderer>();
                    sprRenderer.color = Color.red;
                    if (Input.GetMouseButtonDown(0))
                    {
                        // Remove
                        var towerObj = placedTower[tileCell].gameObject;
                        placedTower.Remove(tileCell);
                        Destroy(towerObj);
                        DisableRemoving();
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && isUpgrading && !levelingUpTowerTile.HasValue)
        {
            var mousePos = Input.mousePosition;
            mousePos.z = parentCanvas.worldCamera.orthographicSize * 2;
            if (!slotChooser.isPlacingTower &&
                mapController.IsPlaceableTile(
                parentCanvas.worldCamera.ScreenToWorldPoint(mousePos), out _, out var tile) &&
                placedTower.ContainsKey(tile))
            {
                levelingUpTowerTile = tile;
            }
        }
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0) && levelingUpTowerTile.HasValue && !upgradeGroup.activeInHierarchy)
        {
            var mousePos = Input.mousePosition;
            mousePos.z = parentCanvas.worldCamera.orthographicSize * 2;
            if (!slotChooser.isPlacingTower &&
                mapController.IsPlaceableTile(
                parentCanvas.worldCamera.ScreenToWorldPoint(mousePos), out _, out var tile) &&
                placedTower.TryGetValue(tile, out var towerData) &&
                !GameManager.HasNoInstance && GameManager.instance.EnoughMoney(towerData.Level * 100))
                DisplayCanvasUpgrade();
        } else if (isUpgrading && !levelingUpTowerTile.HasValue) {
            levelingUpTowerTile = null;
            isUpgrading = false;
            if (upgradeGroup.activeInHierarchy)
                upgradeGroup.SetActive(false);
        }
    }

    void SpawnTower(string evt, params object[] args)
    {
        if (!evt.Equals(SPAWN_TOWER_EVT) || args == null || args.Length < 3)
            return;

        int towerId = (int)args[0];
        Vector3 tilePos = (Vector3)args[1];
        Vector3Int tileCell = (Vector3Int)args[2];

        if (placedTower.ContainsKey(tileCell))
            return;

        var gameObj = Instantiate(TowerResources.Instance.PrefabList.GetTowerPrefab(towerId), tilePos, Quaternion.identity);
        var towerComponent = gameObj.GetComponent<Tower>();
        if (towerComponent != null)
        {
            placedTower.Add(tileCell, towerComponent);
        }
    }

    public void OnClickRemoveBtn()
    {
        if (isRemoving)
            DisableRemoving();
        else
        {
            isRemoving = true;
            previewImg.enabled = true;
        }
    }

    void DisableRemoving()
    {
        isRemoving = false;
        previewImg.enabled = false;
    }

    public void OnLevelingClick(bool allow = true) => isUpgrading = allow;

    // Display canvas upgrade
    private void DisplayCanvasUpgrade()
    {
        // pause game
        Time.timeScale = 0f;
        // display canvas upgrade
        upgradeGroup.SetActive(true);
    }

    public void FinishLevelingUp()
    {
        isUpgrading = false;
        // unpause game
        Time.timeScale = 1f;
        levelingUpTowerTile = null;
        // hide canvas upgrade
        upgradeGroup.SetActive(false);
    }

    void SaveTower(string evt, params object[] args)
    {
        List<RuntimeTowerData> saveData = new List<RuntimeTowerData>(placedTower.Count);
        foreach(var tower in placedTower)
        {
            saveData.Add(new RuntimeTowerData
            {
                tile = tower.Key,
                towerId = tower.Value.Id,
                level = tower.Value.Level,
                range = tower.Value.Range,
                cd = tower.Value.CoolDownTime,
                damage = tower.Value.Damage,
            });
        }
        PlayerPrefs.SetString(PLAYERPREF_SAVEDATA, JsonConvert.SerializeObject(saveData));
        PlayerPrefs.Save();
    }

    void LoadTower(string evt, params object[] args)
    {
        if (PlayerPrefs.HasKey(PLAYERPREF_SAVEDATA))
        {
            var savedTower = JsonConvert.DeserializeObject<RuntimeTowerData[]>(PlayerPrefs.GetString(PLAYERPREF_SAVEDATA));
            if (savedTower != null && savedTower.Length > 0)
            {
                if (placedTower.Count > 0)
                {
                    foreach (var tower in placedTower)
                    {
                        if (tower.Value != null && tower.Value.gameObject != null)
                            Destroy(tower.Value.gameObject);
                    }
                    placedTower.Clear();
                }

                foreach (var tower in savedTower)
                {
                    if (mapController.IsPlaceableTile(tower.tile, out Vector3 spawnPos))
                    {
                        var gameObj = Instantiate(TowerResources.Instance.PrefabList.GetTowerPrefab(tower.towerId), spawnPos, Quaternion.identity);
                        if (gameObj.GetComponent<Tower>() == null)
                        {
                            Destroy(gameObj);
                            continue;
                        }
                        gameObj.GetComponent<Tower>().LoadOldData(tower.level, tower.damage, tower.range, tower.cd);
                        placedTower.Add(tower.tile, gameObj.GetComponent<Tower>());
                    }
                }
            }
        }
    }

    public struct RuntimeTowerData
    {
        public Vector3Int tile { get; set; }
        public int towerId { get; set; }
        public int level { get; set; }
        public int damage { get; set; }
        public float cd { get; set; }
        public float range { get; set; }
    }
}
