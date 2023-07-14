using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ShieldEnemy : MonoBehaviour
    {
        public bool IsActive { get; private set; }
        public bool IsActivedEnable { get; set; }
        public ParticleSystem shieldFx;

        private float duration;
        private void Awake()
        {
            IsActivedEnable = false;
        }

        public void ActivateShield(float duration)
        {
            IsActive = true;
            this.duration = duration;
            IsActivedEnable = true;
            if (shieldFx != null)
                shieldFx.Play();
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
            if (shieldFx != null)
                shieldFx.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            // Các thao tác để huỷ kích hoạt Shield
        }
    }
}

