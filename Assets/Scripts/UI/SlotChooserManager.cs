using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SlotChooserManager : MonoBehaviour
{
    [SerializeField]
    private CameraMovement test;
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

    void OnSlotClick(string evt, params object[] args)
    {
        if (!evt.Equals(SlotData.SLOT_CLICK_EVT) || args == null || args.Length < 4)
            return;

        if (isPreviewingTower)
        {
            DisablePreviewTower();
            return;
        }

        GameObject prefabs = (GameObject)args[0];
        PointerEventData clickData = (PointerEventData)args[1];
        int cost = (int)args[2];
        Sprite prevImg = (Sprite)args[3];

        Debug.Log("Prefabs is null: " + prefabs == null);
        Debug.Log("clickData is null: " + clickData == null);
        Debug.Log("cost is 0: " + (cost == 0));
        Debug.Log("prevImg is null: " + prevImg == null);
        Debug.Log("WOOOOOO");

        if (test != null)
            test.SetScrolling(false);
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
