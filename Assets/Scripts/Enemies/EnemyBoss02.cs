using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss02 : Enemy01_Base
{
    ShieldEnemy sheldEnemy;
    public EnemyBoss02(decimal hp, decimal atk, float speed, decimal money) : base(hp, atk, speed, money)
    {
        sheldEnemy = gameObject.AddComponent<ShieldEnemy>();
    }

    public override bool OnUpdate()
    {
        if (Hp < Hp / 2)
        {
            Atk += 40;
            // miễn sát thương 3s
            // nên tạo lớp sheld
            // check sheld đã active chưa
            // actived -> ..
            if (sheldEnemy != null && !sheldEnemy.IsActive)
            {
                sheldEnemy.ActivateShield(3f);
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
