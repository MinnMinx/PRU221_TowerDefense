using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss03 : Enemy01_Base
{
    SheldEnemy sheldEnemy;
    public EnemyBoss03(decimal hp, decimal atk, float speed, decimal money) : base(hp, atk, speed, money)
    {
        sheldEnemy = gameObject.AddComponent<SheldEnemy>();
    }

    public override bool OnUpdate()
    {
        if (Hp >= Hp / 2)
        {
            Speed += 3.5f;
        }
        else
        {
            Atk += 40;

            // miễn sát thương 3s
            if (sheldEnemy != null && !sheldEnemy.IsActive)
            {
                sheldEnemy.ActivateShield(3f);
            }
        }
        return base.OnUpdate();
    }

    public override void TakeDamage(decimal damage)
    {
        if(!sheldEnemy.IsActive)
            base.TakeDamage(damage);
    }
}
