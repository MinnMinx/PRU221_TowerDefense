using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy07 : Enemy01_Base
{
    private void Awake()
    {
        Hp = 10;
        Atk = 2;
        Speed = 5;
        Money = 2;
    }
}
