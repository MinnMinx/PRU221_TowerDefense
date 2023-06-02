using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBoss01 : Enemy01_Base
    {
        protected override void Awake()
        {
            Hp = 100;
            Atk = 20;
            Speed = 0.35f;
            Money = 50;
            base.Awake();
        }
        public override bool OnUpdate(float deltaTime)
        {
            if (Hp < Hp / 2)
            {
                Speed = 0.75f;
            }
            return base.OnUpdate(deltaTime);
        }
    }
}

