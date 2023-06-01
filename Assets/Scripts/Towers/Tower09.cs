using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower09 : Tower
{
    override protected void Start()
    {
        // set the data of tower
        id = ConfigurationData.ListTower[8].id;
        level = ConfigurationData.ListTower[8].level;
        cost = ConfigurationData.ListTower[8].cost;
        damage = ConfigurationData.ListTower[8].damage;
        range = ConfigurationData.ListTower[8].range;
        muzzleSpeed = ConfigurationData.ListTower[8].muzzleSpeed;
        coolDownTime = ConfigurationData.ListTower[8].coolDownTime;
        base.Start();
    }
}
