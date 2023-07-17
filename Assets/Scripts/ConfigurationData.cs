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
#if UNITY_EDITOR
        string path = "Assets/StreamingAssets/" + configurationFileName;
#else
        string path = Path.Combine(Application.streamingAssetsPath, configurationFileName);
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
        UnityWebRequest www = UnityWebRequest.Get(path);
        www.SendWebRequest().completed += operation =>
        {
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error loading data: " + www.error);
            }
            else
            {
                try
                {
                    string json = www.downloadHandler.text;
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
        };
#else
        LoadDataFromFile(path);
#endif
    }

    private static void LoadDataFromFile(string path)
    {
        try
        {
            string json = File.ReadAllText(path);
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
