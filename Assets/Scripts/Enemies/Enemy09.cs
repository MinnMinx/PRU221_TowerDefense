using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy
{
    public class Enemy09 : Enemy01_Base
    {
        protected override void Awake()
        {
            Hp = 20;
            Atk = 1;
            Speed = 0.35f;
            Money = 3;
            base.Awake();
        }
    }
}
