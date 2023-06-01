using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy
{
    public class Enemy10 : Enemy01_Base
    {
        public void Awake()
        {
            Hp = 20;
            Atk = 3;
            Speed = 2f;
            Money = 2;
        }
    }
}
