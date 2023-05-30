using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower01 : Tower
{
    override protected void Start()
    {
        /* TODO: set up another proprieties */
        level = 1;
        fireRate = 0.1f;
        damage = 10;
        range = 5;
        cost = 10;
        coolDownTime = 1;
        base.Start();
    }
}
