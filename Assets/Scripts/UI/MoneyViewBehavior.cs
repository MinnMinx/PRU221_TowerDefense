using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyViewBehavior : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyLbl;
    [SerializeField]
    private Animator moneyAnimator;
    [SerializeField]
    private ParticleSystem gainMoneyEffect;
    private ParticleSystem.Burst gainFxBurst;

    public static string EVT_MONEY_INSUFFICIENT = "EVT_MONEY_INSUFFICIENT";
    public static string EVT_MONEY_GAIN = "EVT_MONEY_GAIN";
    public static string EVT_MONEY_UPDATE_VIEW = "EVT_MONEY_UPDATE_VIEW";

    // Start is called before the first frame update
    void Start()
    {
        moneyLbl.text = GameManager.instance.money.ToString();
        gainFxBurst = gainMoneyEffect.emission.GetBurst(0);
        GameEventManager.Instance.RegisterEvent(EVT_MONEY_GAIN, OnGainMoney);
        GameEventManager.Instance.RegisterEvent(EVT_MONEY_INSUFFICIENT, OnNotifyMoneyInsufficient);
        GameEventManager.Instance.RegisterEvent(EVT_MONEY_UPDATE_VIEW, OnUpdateLabel);
    }

    void OnNotifyMoneyInsufficient(string evt, params object[] args)
    {
        if (!evt.Equals(EVT_MONEY_INSUFFICIENT))
            return;

        moneyAnimator.Play("InsufficientNotify", 0, 0);
    }

    void OnGainMoney(string evt, params object[] args)
    {
        if (!evt.Equals(EVT_MONEY_GAIN))
            return;
        if (args != null && args.Length > 0)
        {
            float money = (float)args[0];
            gainFxBurst.count = Mathf.Max(3, Mathf.Min(money, 15f));
            gainMoneyEffect.emission.SetBurst(0, gainFxBurst);
        }

        moneyAnimator.Play("GainMoney", 0, 0);
        gainMoneyEffect.Play();
        moneyLbl.text = GameManager.instance.money.ToString();
        gainFxBurst.count = 5;
        gainMoneyEffect.emission.SetBurst(0, gainFxBurst);
    }

    void OnUpdateLabel(string evt, params object[] args)
    {
        if (string.IsNullOrEmpty(evt))
            return;
        decimal money = 0;
        if (args != null && args.Length > 0)
            money = (decimal)args[0];
        else
            money = GameManager.instance.money;
        moneyLbl.text = money.ToString();
    }
}
