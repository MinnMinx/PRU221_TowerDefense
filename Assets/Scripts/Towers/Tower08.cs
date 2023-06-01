using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower08 : Tower
{
    override protected void Start()
    {
        // set the data of tower
        id = ConfigurationData.ListTower[7].id;
        level = ConfigurationData.ListTower[7].level;
        cost = ConfigurationData.ListTower[7].cost;
        damage = ConfigurationData.ListTower[7].damage;
        range = ConfigurationData.ListTower[7].range;
        muzzleSpeed = ConfigurationData.ListTower[7].muzzleSpeed;
        coolDownTime = ConfigurationData.ListTower[7].coolDownTime;
        base.Start();
    }
}
