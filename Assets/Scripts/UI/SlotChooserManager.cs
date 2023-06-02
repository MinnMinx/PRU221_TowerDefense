using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SlotChooserManager : MonoBehaviour
{
    [SerializeField]
    private MapController mapController;
    [SerializeField]
    private GameObject slotChooserPrefab;
    [SerializeField]
    private Transform slotChooserParent;
    [SerializeField]
    private Image previewImage;
    [SerializeField]
    private Canvas parentCanvas;
    private int? prevTowerId;

    private const float PREVIEW_ALPHA = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        GameUiEventManager.Instance.RegisterEvent(SlotData.SLOT_CLICK_EVT, OnSlotClick);
        DisablePreviewTower();
        SpawnSlots();
    }

    private void Update()
    {
        if (prevTowerId.HasValue)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(parentCanvas.transform as RectTransform,
                    Input.mousePosition, parentCanvas.worldCamera, out Vector3 mousePos))
            {
                previewImage.transform.position = mousePos;
                bool canPlace = mapController.IsPlaceableTile(mousePos, out Vector3 tilePos, out Vector3Int tileCell);
                Color prevColor = canPlace ? Color.white : Color.red;
                prevColor.a = canPlace ? PREVIEW_ALPHA : PREVIEW_ALPHA / 2;
                previewImage.color = prevColor;
                if (Input.GetMouseButtonDown(0))
                {
                    if (canPlace)
                    {
                        GameManager.instance.SpendNewTower(prevTowerId.Value, tilePos, tileCell);
                    }
                    DisablePreviewTower();
                }
            }
        }
    }

    void SpawnSlots()
    {
        var towerPrevData = TowerResources.Instance.PreviewAsset;
        for (int i = 0; i < towerPrevData.Count; i++)
        {
            var go = Instantiate(slotChooserPrefab, slotChooserParent);
            var slotData = go.GetComponent<SlotData>();
            if (slotData != null)
            {
                var tower = ConfigurationData.ListTower.FirstOrDefault(tower => tower.id == towerPrevData[i].towerId);
                slotData.Init(towerPrevData[i].towerId,
                                towerPrevData[i].prevSprite,
                                tower == null ? 0 : tower.cost);
            }
        }
    }

    void OnSlotClick(string evt, params object[] args)
    {
        if (!evt.Equals(SlotData.SLOT_CLICK_EVT) || args == null || args.Length < 4)
            return;

        if (prevTowerId.HasValue)
        {
            DisablePreviewTower();
            return;
        }

        int towerId = (int)args[0];
        int cost = (int)args[1];
        Sprite prevImg = (Sprite)args[2];

        GameUiEventManager.Instance.Notify(CameraMovement.CAMERA_SET_MOVEMENT, false);
        prevTowerId = towerId;
        previewImage.enabled = true;
        previewImage.sprite = prevImg;
        previewImage.preserveAspect = true;
    }

    void DisablePreviewTower()
    {
        previewImage.enabled = false;
        prevTowerId = null;
    }

    public bool isPlacingTower => prevTowerId.HasValue;
}
