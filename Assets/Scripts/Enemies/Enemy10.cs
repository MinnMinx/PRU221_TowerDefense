using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy
{
    public class Enemy10 : Enemy01_Base
    {
        protected override void Awake()
        {
            Hp = 20;
            Atk = 3;
            Speed = 0.75f;
            Money = 20;
            base.Awake();
        }
    }
}
