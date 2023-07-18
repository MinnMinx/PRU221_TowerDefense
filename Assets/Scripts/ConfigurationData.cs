using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigurationData : MonoBehaviour
{
    private static List<Towers> towersList;
    private const string configurationFileName = "ConfigurationData.json";

    public static List<Towers> ListTower
    {
        get
        {
            if (towersList == null)
                LoadData();
            return towersList;
        }
    }

    private static void LoadData()
    {
        string path = Path.Combine(Application.streamingAssetsPath, configurationFileName);

        if (path.Contains("://"))
        {
            UnityWebRequest www = UnityWebRequest.Get(path);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.timeout = 180;
            var asyncOp = www.SendWebRequest();
            asyncOp.completed += operation =>
            {
                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Error loading data: " + www.error);
                }
                else
                {
                    LoadData(www.downloadHandler.text);
                }
            };
            while (!asyncOp.isDone) ;
            www.Dispose();
        }
        else
        {
            LoadData(File.ReadAllText(path));
        }
    }

    private static void LoadData(string json)
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
