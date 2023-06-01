using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower04 : Tower
{
    override protected void Start()
    {
        // set the data of tower
        id = ConfigurationData.ListTower[3].id;
        level = ConfigurationData.ListTower[3].level;
        cost = ConfigurationData.ListTower[3].cost;
        damage = ConfigurationData.ListTower[3].damage;
        range = ConfigurationData.ListTower[3].range;
        muzzleSpeed = ConfigurationData.ListTower[3].muzzleSpeed;
        coolDownTime = ConfigurationData.ListTower[3].coolDownTime;
        base.Start();
    }
}
