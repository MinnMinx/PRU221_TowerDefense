using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Modifier
    {
        public float timeLeft { get; set; }
        public float multipler { get; set; }
        public ModifierType type { get; set; }
        public enum ModifierType
        {
            Heal,
            Spd,
            Dmg_Multipler,
        }
    }
}

