using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBoss01 : Enemy01_Base
    {
        public EnemyBoss01(decimal hp, decimal atk, float speed, decimal money) : base(hp, atk, speed, money)
        {
        }

        public override bool OnUpdate()
        {
            if (Hp < Hp / 2)
            {
                Speed += 3.5f;
            }
            return base.OnUpdate();
        }
    }
}

