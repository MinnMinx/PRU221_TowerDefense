using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy05 : Enemy01_Base
    {
        protected override void Awake()
        {
            Hp = 30;
            Atk = 5;
            Speed = 0.5f;
            Money = 30;
            base.Awake();
        }
    }
}

