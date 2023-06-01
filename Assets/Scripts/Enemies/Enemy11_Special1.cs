using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy11_Special1 : Enemy01_Base
    {
        protected override void Awake()
        {
            canSpeed = true;
            Hp = 10;
            Atk = 3;
            Speed = 1f;
            Money = 5;
            base.Awake();
        }
    }
}
    
