using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower01 : Tower
{
    override protected void Start()
    {
        id = ConfigurationData.ListTower[0].id;
        level = ConfigurationData.ListTower[0].level;
        cost = ConfigurationData.ListTower[0].cost;
        damage = ConfigurationData.ListTower[0].damage;
        range = ConfigurationData.ListTower[0].range;
        muzzleSpeed = ConfigurationData.ListTower[0].muzzleSpeed;
        coolDownTime = ConfigurationData.ListTower[0].coolDownTime;

        base.Start();
    }
}
