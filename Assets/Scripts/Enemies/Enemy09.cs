using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy
{
    public class Enemy09 : Enemy01_Base
    {
        public void Awake()
        {
            Hp = 20;
            Atk = 1;
            Speed = 2f;
            Money = 3;
        }
    }
}
