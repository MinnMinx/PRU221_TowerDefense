using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower06 : Tower
{
    override protected void Start()
    {
        // set the data of tower
        id = ConfigurationData.ListTower[5].id;
        level = ConfigurationData.ListTower[5].level;
        cost = ConfigurationData.ListTower[5].cost;
        damage = ConfigurationData.ListTower[5].damage;
        range = ConfigurationData.ListTower[5].range;
        muzzleSpeed = ConfigurationData.ListTower[5].muzzleSpeed;
        coolDownTime = ConfigurationData.ListTower[5].coolDownTime;
        base.Start();
    }
}
