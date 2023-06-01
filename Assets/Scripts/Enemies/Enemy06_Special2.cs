using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy06_Special2 : Enemy01_Base
{
    //
    private float timerEachShield = 5f;
    private void Awake()
    {
        Hp = 20;
        Atk = 3;
        Speed = 2;
        Money = 2;
    }

    // shield will enable each 5s
    ShieldEnemy sheldEnemy;
    public void Start()
    {
        sheldEnemy = gameObject.AddComponent<ShieldEnemy>();
    }

    public override bool OnUpdate()
    {
        timerEachShield -= Time.deltaTime;
        if(timerEachShield <= 0f)
        {
            if (sheldEnemy != null && !sheldEnemy.IsActive)
            {
                sheldEnemy.ActivateShield(1f);
                timerEachShield = 5f;
                // nhận dame thì bullet sẽ check xem active hay không
            }
        }
        return base.OnUpdate();
    }

    public override void TakeDamage(decimal damage)
    {
        if (!sheldEnemy.IsActive)
            base.TakeDamage(damage);
    }
}
