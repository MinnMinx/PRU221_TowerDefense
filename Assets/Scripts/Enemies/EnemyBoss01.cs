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
            Speed = 2.5f;
            Money = 50;
            base.Awake();
        }
        public override bool OnUpdate()
        {
            Debug.Log("avxc");
            if (Hp < Hp / 2)
            {
                Speed = 3.5f;
            }
            return base.OnUpdate();
        }
    }
}

