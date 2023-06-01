using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotData : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Sprite previewSpr;
    [SerializeField]
    private int cost;
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private Image prevImage;
    [SerializeField]
    private TextMeshProUGUI costLbl;

    public static string SPAWN_EVENT_NAME = "SLOT_SPAWN";
    public static string SLOT_CLICK_EVT = "SLOT_CLICK_EVT";

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            prevImage.sprite = previewSpr;
            costLbl.text = cost.ToString();
        }
        catch
        {
            Debug.LogError("Error starting up UI slot: " + gameObject.name);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameUiEventManager.Instance.Notify(SLOT_CLICK_EVT, prefab, eventData, cost, previewSpr);
    }
}
