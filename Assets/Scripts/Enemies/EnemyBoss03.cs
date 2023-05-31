using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss03 : Enemy01_Base
{
    public EnemyBoss03(decimal hp, decimal atk, float speed, decimal money) : base(hp, atk, speed, money)
    {
    }

    public override bool OnUpdate()
    {
        if (Hp >= Hp / 2)
        {
            Speed += 3.5f;
        }
        else
        {
            Atk += 40;

            // miễn sát thương 3s
        }
        return base.OnUpdate();
    }
}
