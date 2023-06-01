using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy11_Special1 : Enemy01_Base
    {
        private void Start()
        {
        }

        public void Awake()
        {
            Hp = 10;
            Atk = 3;
            Speed = 4f;
            Money = 5;
        }
    }
}
    
