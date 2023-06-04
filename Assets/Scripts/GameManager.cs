using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SlotChooserManager slotChoseMng;
    [SerializeField]
    private TowerManager towerMng;

    [SerializeField]
    private HealthBarBehaviour healthBarBehaviour;

    public decimal money = 300;
    public decimal score;
    public decimal playerHp;
    public decimal maxPlayerHp = 100;

    private static GameManager _instance;

    public bool IsPlacingTower => slotChoseMng.isPlacingTower;
    public bool IsRemovingTower => towerMng.IsRemovingTower;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            playerHp = maxPlayerHp;
            healthBarBehaviour.SetHealth(Convert.ToSingle(playerHp), Convert.ToSingle(maxPlayerHp));
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (playerHp <= 0)
        {
            Debug.Log("you lose");
        }
    }

    public void TakeDamage(decimal atk)
    {
        playerHp -= atk;
        healthBarBehaviour.SetHealth(Convert.ToSingle(playerHp), Convert.ToSingle(maxPlayerHp));
    }

    public void GainMoney(decimal money)
    {
        this.money += money;
        GameUiEventManager.Instance.Notify(MoneyViewBehavior.EVT_MONEY_GAIN, (float)money);
    }

    public void GainScore(decimal score)
    {
        this.score += score;
    }

    public void SpendNewTower(int towerId, Vector3 tilePos, Vector3Int tileCell)
    {
        var tower = ConfigurationData.ListTower.FirstOrDefault(tower => tower.id == towerId);
        if (tower != null && money >= tower.cost)
        {
            money -= tower.cost;
            GameUiEventManager.Instance.Notify(TowerManager.SPAWN_TOWER_EVT, towerId, tilePos, tileCell);
            GameUiEventManager.Instance.Notify(MoneyViewBehavior.EVT_MONEY_UPDATE_VIEW, money);
        }
        else if (tower != null)
        {
            // not enough money
            GameUiEventManager.Instance.Notify(MoneyViewBehavior.EVT_MONEY_INSUFFICIENT);
        }
    }

    private void OnDestroy()
    {
        GameUiEventManager.Instance.Clear();
    }
}
