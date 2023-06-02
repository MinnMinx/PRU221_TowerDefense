using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerPrefabList", menuName = "Tower/List of Tower prefabs", order = 6)]
public class TowerPrefabAsset : ScriptableObject
{
    [SerializeField]
    private TowerPrefab[] prefabList;

    public int Count => prefabList.Length;
    public TowerPrefab this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                return null;
            return prefabList[index];
        }
    }

    public GameObject GetTowerPrefab(int towerId)
    {
        for (int i = 0; i < Count; i++)
        {
            if (prefabList[i].id == towerId)
                return prefabList[i].prefab;
        }
        return null;
    }

    [System.Serializable]
    public class TowerPrefab
    {
        public int id;
        public GameObject prefab;
    }
}
