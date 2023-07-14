using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy07 : Enemy01_Base
    {
        protected override void Awake()
        {
            Hp = 20;
            Atk = 2;
            Speed = 1.75f;
            Money = 20;
            base.Awake();
        }
    }
}

