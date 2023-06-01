using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy05 : Enemy01_Base
{
    protected override void Awake()
    {
        Hp = 15;
        Atk = 5;
        Speed = 1f;
        Money = 5;
        base.Awake();
    }
}
