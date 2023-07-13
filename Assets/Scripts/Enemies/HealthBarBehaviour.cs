using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class HealthBarBehaviour : MonoBehaviour
    {
        public Slider slider;
        public Color low;
        public Color high;
        public Vector3 offset;

        public void SetHealth(float healthPercent)
        {
            slider.value = healthPercent;
        }
    }

}
