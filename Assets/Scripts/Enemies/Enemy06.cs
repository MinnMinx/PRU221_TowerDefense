using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy06 : Enemy01_Base
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        Hp = 20;
        Atk = 3;
        Speed = 2;
        Money = 2;
    }
}
