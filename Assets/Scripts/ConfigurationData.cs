using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ConfigurationData : MonoBehaviour
{
    private static List<Towers> towersList;
    private const string CONFIG_NAME = "ConfigurationData";

    public static List<Towers> ListTower
    {
        get
        {
            if (towersList == null)
            {
                var textAsset = Resources.Load<TextAsset>(CONFIG_NAME);
                LoadData(textAsset.text);
            }
            return towersList;
        }
    }

    public static void LoadData(string json)
    {
        try
        {
            towersList = JsonUtility.FromJson<Serialization<Towers>>(json)?.listTower;

            if (towersList == null)
            {
                Debug.LogError("Error deserializing data: Unable to parse JSON or missing required fields.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error loading data: " + e.Message);
        }
    }

    [Serializable]
    private class Serialization<T>
    {
        public List<T> listTower;

        public Serialization()
        {
            listTower = new List<T>();
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
