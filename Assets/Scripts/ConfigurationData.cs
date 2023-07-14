using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigurationData
{
    // List of towers
    private static List<Towers> towersList;

    /// <summary>
    /// If the inner list isn't load, load data
    /// </summary>
    public static List<Towers> ListTower
    {
        get
        {
            if (towersList == null)
                LoadData();
            return towersList;
        }
    }

    /// <summary>
    /// Load the configuration tower data to the list
    /// </summary>
    //public static void Initialize()
    //{
    //    LoadData();
    //}

    /// <summary>
    /// Load the configuration tower data from the json file
    /// </summary>
    private static void LoadData()
    {
        // load all tower data from ConfigurationData.json
        string json = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/ConfigurationData.json");

        try
        {
            towersList = JsonUtility.FromJson<Serialization<Towers>>(json).ToList();
        }
        catch (Exception e)
        {
            Debug.Log("Error loading data: " + e.Message);
        }
    }

    // A generic wrapper class to help with JSON serialization
    [System.Serializable]
    private class Serialization<T>
    {
        public List<T> listTower;

        public Serialization()
        {
            listTower = new List<T>();
        }

        public Serialization(List<T> list)
        {
            this.listTower = list;
        }

        public List<T> ToList()
        {
            return listTower;
        }
    }

    [Serializable]
    public class Towers
    {
        public int id;
        public int level;
        public int cost;
        public int damage;
        public float range;
        public float muzzleSpeed;
        public float coolDownTime;

        public Towers(int id, int level, int cost, int damage, float range, float muzzleSpeed, float coolDownTime)
        {
            this.id = id;
            this.level = level;
            this.cost = cost;
            this.damage = damage;
            this.range = range;
            this.muzzleSpeed = muzzleSpeed;
            this.coolDownTime = coolDownTime;
        }
    }
}
