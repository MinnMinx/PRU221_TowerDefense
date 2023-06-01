using UnityEngine;
using UnityEngine.UI;

public class SlotChooserManager : MonoBehaviour
{
    [SerializeField]
    private GameObject slotChooserPrefab;
    [SerializeField]
    private Transform slotChooserParent;
    [SerializeField]
    private Image previewImage;
    [SerializeField]
    private Canvas parentCanvas;
    private bool isPreviewingTower;

    // Start is called before the first frame update
    void Start()
    {
        GameUiEventManager.Instance.RegisterEvent(SlotData.SLOT_CLICK_EVT,
                    new System.Action<string, object[]>(OnSlotClick));
        isPreviewingTower = false;
        SpawnSlots();
    }

    private void Update()
    {
        if (isPreviewingTower)
        {
            previewImage.enabled = true;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(parentCanvas.transform as RectTransform,
                    Input.mousePosition, parentCanvas.worldCamera, out Vector3 mousePos))
            {
                previewImage.transform.position = mousePos;
                if (Input.GetMouseButtonDown(0))
                {
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
                slotData.Init(towerPrevData[i].towerId,
                                towerPrevData[i].prevSprite,
                                Random.Range(0, 101));
            }
        }
    }

    void OnSlotClick(string evt, params object[] args)
    {
        if (!evt.Equals(SlotData.SLOT_CLICK_EVT) || args == null || args.Length < 4)
            return;

        if (isPreviewingTower)
        {
            DisablePreviewTower();
            return;
        }

        int towerId = (int)args[0];
        int cost = (int)args[1];
        Sprite prevImg = (Sprite)args[2];

        GameUiEventManager.Instance.Notify(CameraMovement.CAMERA_SET_MOVEMENT, false);
        isPreviewingTower = true;
        previewImage.sprite = prevImg;
        previewImage.preserveAspect = true;
    }

    void DisablePreviewTower()
    {
        previewImage.enabled = false;
        isPreviewingTower = false;
    }
}
