using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy07 : Enemy01_Base
{
    protected override void Awake()
    {
        Hp = 10;
        Atk = 2;
        Speed = 0.5f;
        Money = 2;
        base.Awake();
    }
}
