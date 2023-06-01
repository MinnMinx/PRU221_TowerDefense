using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower03 : Tower
{
    override protected void Start()
    {
        // set the data of tower
        id = ConfigurationData.ListTower[2].id;
        level = ConfigurationData.ListTower[2].level;
        cost = ConfigurationData.ListTower[2].cost;
        damage = ConfigurationData.ListTower[2].damage;
        range = ConfigurationData.ListTower[2].range;
        muzzleSpeed = ConfigurationData.ListTower[2].muzzleSpeed;
        coolDownTime = ConfigurationData.ListTower[2].coolDownTime;

        base.Start();
    }
}
