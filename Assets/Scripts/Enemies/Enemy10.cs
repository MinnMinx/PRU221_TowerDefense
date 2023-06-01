using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy
{
    public class Enemy10 : Enemy01_Base
    {
        public override void Awake()
        {
            Hp = 20;
            Atk = 3;
            Speed = 2f;
            Money = 2;
            base.Awake();
        }

        // Start is called before the first frame update
        override protected void Start()
        {
            
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
