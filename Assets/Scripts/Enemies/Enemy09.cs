using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy
{
    public class Enemy09 : Enemy01_Base
    {
        protected override void Awake()
        {
            Hp = 30;
            Atk = 1;
            Speed = 0.75f;
            Money = 20;
            base.Awake();
        }
    }
}
