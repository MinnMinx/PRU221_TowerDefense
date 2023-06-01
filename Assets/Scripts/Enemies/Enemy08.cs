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
            Hp = 10;
            Atk = 1;
            Speed = 1f;
            Money = 3;
            base.Awake();
        }
    }
}
