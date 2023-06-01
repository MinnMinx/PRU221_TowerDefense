using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy05 : Enemy01_Base
{
    public void Awake()
    {
        Hp = 15;
        Atk = 5;
        Speed = 2f;
        Money = 5;
    }
}
