using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy06_Special2 : Enemy01_Base
{
    private void Awake()
    {
        Hp = 20;
        Atk = 3;
        Speed = 2;
        Money = 2;
    }
}
