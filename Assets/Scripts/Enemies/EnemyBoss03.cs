using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBoss03 : Enemy01_Base
    {
        ShieldEnemy sheldEnemy;
        public void Start()
        {
            sheldEnemy = gameObject.AddComponent<ShieldEnemy>();
        }

        public override bool OnUpdate()
        {
            if (Hp >= Hp / 2)
            {
                Speed = 3.5f;
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
            if (!sheldEnemy.IsActive)
                base.TakeDamage(damage);
        }
    }
}
