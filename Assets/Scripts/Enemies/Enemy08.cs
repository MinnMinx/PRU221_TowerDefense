using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy
{
    public class Enemy08 : Enemy01_Base
    {
        // Start is called before the first frame update
        protected override void Awake()
        {
            Hp = 20;
            Atk = 5;
            Speed = 1.5f;
            Money = 30;
            base.Awake();
        }
    }
}
