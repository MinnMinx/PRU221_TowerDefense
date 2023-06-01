using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower02 : Tower
{
    override protected void Start()
    {
        //Debug.Log(ConfigurationData.ListTower.Count);
        id = ConfigurationData.ListTower[1].id;
        level = ConfigurationData.ListTower[1].level;
        cost = ConfigurationData.ListTower[1].cost;
        damage = ConfigurationData.ListTower[1].damage;
        range = ConfigurationData.ListTower[1].range;
        muzzleSpeed = ConfigurationData.ListTower[1].muzzleSpeed;
        coolDownTime = ConfigurationData.ListTower[1].coolDownTime;
        base.Start();
    }
}
