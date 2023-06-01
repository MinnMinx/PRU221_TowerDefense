using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower02 : Tower
{
    override protected void Start()
    {
        Debug.Log(ConfigurationData.ListTower.Count);
        id = ConfigurationData.ListTower[1].Id;
        level = ConfigurationData.ListTower[1].Level;
        cost = ConfigurationData.ListTower[1].Cost;
        damage = ConfigurationData.ListTower[1].Damage;    
        range = ConfigurationData.ListTower[1].Range;
        muzzleSpeed = ConfigurationData.ListTower[1].MuzzleSpeed;
        coolDownTime = ConfigurationData.ListTower[1].CoolDownTime;
        base.Start();
    }
}
