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
            Hp = 20;
            Atk = 3;
            Speed = 2.65f;
            Money = 30;
            base.Awake();
        }
    }
}
    
