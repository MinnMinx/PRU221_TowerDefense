using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Enemy
{
    public class Enemy06_Special2 : Enemy01_Base
    {
        //
        private float timerEachShield = 5f;

        // shield will enable each 5s
        ShieldEnemy sheldEnemy;
        protected override void Awake()
        {
            Hp = 20;
            Atk = 3;
            Speed = 1f;
            Money = 30;
            sheldEnemy = gameObject.AddComponent<ShieldEnemy>();
            base.Awake();
        }


        public override bool OnUpdate(float deltaTime)
        {
            timerEachShield -= deltaTime; // Time.deltaTime;
            if (timerEachShield <= 0f)
            {
                if (sheldEnemy != null && !sheldEnemy.IsActive)
                {
                    sheldEnemy.ActivateShield(1f);
                    timerEachShield = 5f;
                    // nhận dame thì bullet sẽ check xem active hay không
                }
            }
            return base.OnUpdate(deltaTime);
        }

        public override void TakeDamage(decimal damage)
        {
            if (!sheldEnemy.IsActive)
                base.TakeDamage(damage);
        }
    }
}

