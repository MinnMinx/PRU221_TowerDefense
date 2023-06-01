using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy
{
    public class Enemy08 : Enemy01_Base
    {
        // Start is called before the first frame update
        public override void Awake()
        {
            Hp = 10;
            Atk = 1;
            Speed = 5f;
            Money = 3;
            base.Awake();
        }
        public void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
