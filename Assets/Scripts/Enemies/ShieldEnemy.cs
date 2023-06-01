using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ShieldEnemy : MonoBehaviour
    {
        public bool IsActive { get; private set; }

        private float duration;

        public void ActivateShield(float duration)
        {
            IsActive = true;
            this.duration = duration;
            // Các thao tác để kích hoạt Shield
        }

        private void Update()
        {
            if (IsActive)
            {
                duration -= Time.deltaTime;
                if (duration <= 0f)
                {
                    DeactivateShield();
                }
            }
        }

        private void DeactivateShield()
        {
            IsActive = false;
            duration = 0f;
            // Các thao tác để huỷ kích hoạt Shield
        }
    }
}

