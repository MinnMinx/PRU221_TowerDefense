using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBoss02 : Enemy01_Base
    {
        ShieldEnemy sheldEnemy;

        protected override void Awake()
        {
            Hp = 200;
            Speed = 0.35f;
            Atk = 25;
            Money = 100;

            sheldEnemy = gameObject.AddComponent<ShieldEnemy>();
            base.Awake();
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

