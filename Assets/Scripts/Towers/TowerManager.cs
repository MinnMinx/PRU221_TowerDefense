using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    [SerializeField]
    private Image previewImg;
    [SerializeField]
    private Canvas parentCanvas;
    [SerializeField]
    private MapController mapController;
    private Dictionary<Vector3Int, Tower> placedTower;
    private bool isRemoving;
    private Vector3Int lastSelectCell;

    public bool IsRemovingTower => isRemoving;

    public static string SPAWN_TOWER_EVT = "SPAWN_TOWER_EVT";

    // Start is called before the first frame update
    void Start()
    {
        placedTower = new Dictionary<Vector3Int, Tower>();
        GameUiEventManager.Instance.RegisterEvent(SPAWN_TOWER_EVT, SpawnTower);
        GameUiEventManager.Instance.RegisterEvent(SlotData.SLOT_CLICK_EVT, delegate
        {
            DisableRemoving();
        });
        DisableRemoving();
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
}
