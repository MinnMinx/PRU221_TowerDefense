using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower07 : Tower
{
    override protected void Start()
    {
        // set the data of tower
        id = ConfigurationData.ListTower[6].id;
        level = ConfigurationData.ListTower[6].level;
        cost = ConfigurationData.ListTower[6].cost;
        damage = ConfigurationData.ListTower[6].damage;
        range = ConfigurationData.ListTower[6].range;
        muzzleSpeed = ConfigurationData.ListTower[6].muzzleSpeed;
        coolDownTime = ConfigurationData.ListTower[6].coolDownTime;
        base.Start();
    }
}
