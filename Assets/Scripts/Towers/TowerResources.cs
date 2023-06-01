using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = FILENAME, menuName = "Tower/Resouces for all", order = 0)]
public class TowerResources : ScriptableObject
{
    private static TowerResources instance;
    private const string FILENAME = "TowerResources";

    public static TowerResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<TowerResources>(FILENAME);
            }
            return instance;
        }
    }

    public TowerPreviewerAsset PreviewAsset;
    public TowerPrefabAsset PrefabList;
}
