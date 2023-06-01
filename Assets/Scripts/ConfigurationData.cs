using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigurationData
{
    public static List<Tower> listTower = new List<Tower>();

    public static List<Tower> ListTower
    {
        get { return listTower; }
    }

    /// <summary>
    /// Load the configuration tower data to the list
    /// </summary>
    public static void Initialize()
    {
        LoadData();
    }

    /// <summary>
    /// Load the configuration tower data from the json file
    /// </summary>
    private static void LoadData()
    {
        // load all tower data from ConfigurationData.json
        string json = System.IO.File.ReadAllText(Application.dataPath + "/Scripts/ConfigurationData.json");

        try
        {
            Towers towers = JsonUtility.FromJson<Towers>(json);
            listTower = towers._listTower;
            Debug.Log("Load data successfully");
            Debug.Log("List tower: " + listTower.Count);
        }
        catch (Exception e)
        {
            Debug.Log("Error loading data: " + e.Message);
        }
    }

    private static void SaveData()
    {
        // save all tower data to ConfigurationData.json
        Tower tower = new Tower
        {
            Id = 1,
            Level = 1,
            Cost = 100,
            Damage = 10,
            Range = 10,
            MuzzleSpeed = 10,
            CoolDownTime = 10
        };
        listTower.Add(tower);
        string json = JsonUtility.ToJson(new Towers(listTower));
        File.WriteAllText(Application.dataPath + "/Scripts/ConfigurationData1.json", json);
    }

    /// <summary>
    /// The class that holds the configuration data
    /// </summary>
    [Serializable]
    public class Towers
    {
        public List<Tower> _listTower;

        public Towers(List<Tower> listTower)
        {
            this._listTower = listTower;
        }
    }
}
