using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy04 : Enemy01_Base
    {
        protected override void Awake()
        {
            Hp = 10;
            Atk = 2;
            Speed = 0.2f;
            Money = 2;
            base.Awake();
        }
    }
}

