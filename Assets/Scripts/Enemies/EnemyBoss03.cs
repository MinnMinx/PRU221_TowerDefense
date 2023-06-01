﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBoss03 : Enemy01_Base
    {
        ShieldEnemy sheldEnemy;
        protected override void Awake()
        {
            Hp = 300;
            Speed = 2.5f;
            Atk = 30;
            Money = 150;

            sheldEnemy = gameObject.AddComponent<ShieldEnemy>();
            base.Awake();
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
