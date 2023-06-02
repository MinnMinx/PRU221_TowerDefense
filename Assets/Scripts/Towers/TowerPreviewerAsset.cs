using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerPreviewAssetList", menuName = "Tower/List Preview Asset", order = 1)]
public class TowerPreviewerAsset : ScriptableObject
{
    [SerializeField]
    private List<TowerAssetPreviewer> previewList = new List<TowerAssetPreviewer>();
    public TowerAssetPreviewer this[int index]
    {
        get
        {
            if (index < 0 || index >= previewList.Count)
                return null;
            return previewList[index];
        }
    }
    public int Count => previewList.Count;

    public TowerAssetPreviewer FindWithId(int id)
    {
        for (int i = 0; i < Count; i++)
        {
            if (previewList[i].towerId == id)
                return previewList[i];
        }
        return null;
    }

    [Serializable]
    public class TowerAssetPreviewer
    {
        public int towerId;
        public Sprite prevSprite;
        public AnimationClip idleAnim;
    }
}
