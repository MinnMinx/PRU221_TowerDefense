using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SlotChooserManager slotChoseMng;

    public decimal money;
    public decimal score;
    public decimal playerHp = 100;

    private static GameManager _instance;

    public bool IsPlacingTower => slotChoseMng.isPlacingTower;

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
    }

    public void GainMoney(decimal money)
    {
        this.money += money;
    }

    public void GainScore(decimal score)
    {
        this.score += score;
    }
}
