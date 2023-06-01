using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy11 : Enemy01_Base
    {
        // Start is called before the first frame update
        override public void Awake()
        {
            Hp = 10;
            Atk = 3;
            Speed = 4f;
            Money = 5;
            base.Awake();
        }
        override protected void Start()
        {
         
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
    
