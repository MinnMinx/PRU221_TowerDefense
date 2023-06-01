using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy
{
    public class Enemy09 : Enemy01_Base
    {
        public override void Awake()
        {
            Hp = 20;
            Atk = 1;
            Speed = 2f;
            Money = 3;
            base.Awake();
        }
        // Start is called before the first frame update
        override protected void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
