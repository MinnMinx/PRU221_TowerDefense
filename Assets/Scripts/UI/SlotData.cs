using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotData : MonoBehaviour, IPointerClickHandler
{
    private int towerId;
    private Sprite previewSpr;
    private int cost;

    [SerializeField]
    private Image prevImage;
    [SerializeField]
    private TextMeshProUGUI costLbl;

    public static string SLOT_CLICK_EVT = "SLOT_CLICK_EVT";

    // Start is called before the first frame update
    public void Init(int towerId, Sprite previewSpr, int cost)
    {
        this.towerId = towerId;
        this.previewSpr = previewSpr;
        this.cost = cost;
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
        GameEventManager.Instance.Notify(SLOT_CLICK_EVT, towerId, cost, previewSpr, eventData);
    }
}
