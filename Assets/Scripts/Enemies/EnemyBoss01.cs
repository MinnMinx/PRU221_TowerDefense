using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBoss01 : Enemy01_Base
    {
        protected override void Awake()
        {
            Hp = 300;
            Atk = 15;
            Speed = 1f;
            Money = 100;
            base.Awake();
        }
        public override bool OnUpdate(float deltaTime)
        {
            if (Hp < Hp / 2)
            {
                Speed = 1f;
            }
            return base.OnUpdate(deltaTime);
        }
    }
}

