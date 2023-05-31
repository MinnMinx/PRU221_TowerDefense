﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss02 : Enemy01_Base
{
    public EnemyBoss02(decimal hp, decimal atk, float speed, decimal money) : base(hp, atk, speed, money)
    {
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
        }
        return base.OnUpdate();
    }
}
