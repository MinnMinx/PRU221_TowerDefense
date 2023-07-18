using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager
{
    private static GameEventManager instance;
    public static GameEventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEventManager();
            }
            return instance;
        }
    }

    private Dictionary<string, List<Action<string, object[]>>> eventDic = new Dictionary<string, List<Action<string, object[]>>>();

    public void Clear()
    {
        this.eventDic.Clear();
    }

    public void Notify(string evt, params object[] args)
    {
        if (string.IsNullOrEmpty(evt))
        {
            return;
        }
        List<Action<string, object[]>> list = null;
        if (this.eventDic.TryGetValue(evt, out list))
        {
            for (int i = 0; i < list.Count; i++)
            {
                Action<string, object[]> action = list[i];
                if (action != null)
                {
                    action(evt, args);
                }
            }
        }
    }

    public void RegisterEvent(string evt, Action<string, object[]> action)
    {
        if (string.IsNullOrEmpty(evt) || action == null)
        {
            return;
        }
        List<Action<string, object[]>> list = null;
        if (!this.eventDic.TryGetValue(evt, out list))
        {
            list = new List<Action<string, object[]>>();
            this.eventDic[evt] = list;
        }
        if (!list.Contains(action))
        {
            list.Add(action);
        }
    }

    public void RemoveEvent(string evt, Action<string, object[]> action)
    {
        if (string.IsNullOrEmpty(evt) || action == null)
        {
            return;
        }
        List<Action<string, object[]>> list = null;
        if (!this.eventDic.TryGetValue(evt, out list))
        {
            list = new List<Action<string, object[]>>();
            this.eventDic[evt] = list;
        }
        list.Remove(action);
    }

}
