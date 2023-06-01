using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower05 : Tower
{
    override protected void Start()
    {
        // set the data of tower
        id = ConfigurationData.ListTower[4].id;
        level = ConfigurationData.ListTower[4].level;
        cost = ConfigurationData.ListTower[4].cost;
        damage = ConfigurationData.ListTower[4].damage;
        range = ConfigurationData.ListTower[4].range;
        muzzleSpeed = ConfigurationData.ListTower[4].muzzleSpeed;
        coolDownTime = ConfigurationData.ListTower[4].coolDownTime;
        base.Start();
    }
}
