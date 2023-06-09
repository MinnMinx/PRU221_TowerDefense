﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBoss03 : Enemy01_Base
    {
        [SerializeField]
        ParticleSystem shieldFx;

        ShieldEnemy sheldEnemy;
        protected override void Awake()
        {
            Hp = 200;
            Speed = 1f;
            Atk = 18;
            Money = 100;

            sheldEnemy = gameObject.AddComponent<ShieldEnemy>();
            sheldEnemy.shieldFx = shieldFx;
            base.Awake();
        }

        public override bool OnUpdate(float deltaTime)
        {
            if (Hp >= MaxHp / 2)
            {
                Speed = 2f;
            }
            else
            {
                Atk = 40;
                Speed = 1f;
                // miễn sát thương 3s
                if (sheldEnemy != null && !sheldEnemy.IsActive && !sheldEnemy.IsActivedEnable)
                {
                    sheldEnemy.ActivateShield(3f);
                }
            }
            return base.OnUpdate(deltaTime);
        }

        public override void TakeDamage(decimal damage)
        {
            if (!sheldEnemy.IsActive)
            {
                base.TakeDamage(damage);
            }
        }
    }
}
