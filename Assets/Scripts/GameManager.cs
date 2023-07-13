using Enemy;
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
    public static decimal STARTING_MONEY = 300;

    public decimal money = STARTING_MONEY;
    public decimal score;
    public decimal playerHp;
    public static decimal MAX_HP = 50;

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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(_instance.gameObject);
            _instance = this;
        }
    }

    private void Start()
    {

        if (PlayerPrefs.HasKey("load"))
        {
            LoadGame();
            PlayerPrefs.DeleteKey("load");
            PlayerPrefs.Save();
        }
        else
        {
            playerHp = MAX_HP;
            healthBarBehaviour.SetHealth(Convert.ToSingle(playerHp / MAX_HP));
        }
    }

    public void TakeDamage(decimal atk)
    {
        playerHp -= atk;
        healthBarBehaviour.SetHealth(Convert.ToSingle(playerHp / MAX_HP));
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

    public static void LoadGame()
    {
        GameUiEventManager.Instance.Notify(TowerManager.LOAD_TOWER_EVT);
        GameUiEventManager.Instance.Notify(EnemyManager01.LOAD_ENEMY_EVT);
        instance.score = PlayerPrefs.HasKey("saved_score") ? (decimal)PlayerPrefs.GetFloat("saved_score") : 0;
        instance.money = PlayerPrefs.HasKey("saved_money") ? (decimal)PlayerPrefs.GetFloat("saved_money") : STARTING_MONEY;
        GameUiEventManager.Instance.Notify(MoneyViewBehavior.EVT_MONEY_UPDATE_VIEW, instance.money);
        instance.playerHp = PlayerPrefs.HasKey("saved_hp") ? (decimal)PlayerPrefs.GetFloat("saved_hp") : MAX_HP;
        instance.healthBarBehaviour.SetHealth(Convert.ToSingle(instance.playerHp / MAX_HP));
    }

    public static void SaveGame()
    {
        GameUiEventManager.Instance.Notify(TowerManager.SAVE_TOWER_EVT);
        GameUiEventManager.Instance.Notify(EnemyManager01.SAVE_ENEMY_EVT);
        PlayerPrefs.SetFloat("saved_score", (float)instance.score);
        PlayerPrefs.SetFloat("saved_money", (float)instance.money);
        PlayerPrefs.SetFloat("saved_hp", (float)instance.playerHp);
        PlayerPrefs.Save();
    }

    public static bool ExistSaveData()
    {
        return PlayerPrefs.HasKey("saved_score") &&
                PlayerPrefs.HasKey("saved_money") &&
                PlayerPrefs.HasKey("saved_hp") &&
                PlayerPrefs.HasKey(TowerManager.PLAYERPREF_SAVEDATA) &&
                System.IO.File.Exists(Application.streamingAssetsPath + "/EnemyData.json");
    }
}
