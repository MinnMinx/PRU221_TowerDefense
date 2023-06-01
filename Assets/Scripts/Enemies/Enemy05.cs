using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy05 : Enemy01_Base
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        Hp = 15;
        Atk = 5;
        Speed = 2f;
        Money = 5;
    }
}
