using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

namespace Enemy
{
    public class Enemy01_Base : MonoBehaviour
    {
        /// <summary>
        /// Health of enemy.
        /// </summary>
        private decimal hp;
        public decimal Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        /// <summary>
        /// Atk of enemy.
        /// </summary>
        private decimal atk;
        public decimal Atk
        {
            get { return atk; }
            set { atk = value; }
        }

        /// <summary>
        /// Speed of enemy.
        /// </summary>
        private float speed;
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        /// <summary>
        /// Money of enemy.
        /// </summary>
        private decimal money;
        public decimal Money
        {
            get { return money; }
            set { money = value; }
        }

        public bool isDead = false;


        // Start is called before the first frame 

        // Update is called once per frame
        void Update()
        {
            if (hp <= 0)
            {
                isDead = true;
            }
        }

        public virtual bool OnUpdate()
        {
            return hp >= 0;
        }

        /// <summary>
        /// Destroy enemy.
        /// </summary>
        public virtual void OnDespawn()
        {
            Destroy(this.gameObject);
        }

        /// <summary>
        /// check hit the bullet.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //var a = collision.gameObject.GetComponent<Bullet>();
            //if (a != null)
            //{
            //    TakeDamage(a.Atk);
            //}

            //var b = collision.gameObject.GetComponent<Base>();
            //if (b != null)
            //{
            //    DealDamage();
            //}
        }

        /// <summary>
        /// deal damage to enemy.
        /// </summary>
        /// <param name="damage"></param>
        public virtual void TakeDamage(decimal damage)
        {
            hp = hp - damage;
        }

        public bool DealDamage(Vector3 vector3)
        {
            float distance = Vector3.Distance(gameObject.transform.position, vector3);
            if (distance <= 0.1f) 
                return true;
            return false;
        }

        // set hp theo wave cho enemy
        public decimal SetHp(int wave, double heso)
        {
            decimal a = (decimal)Math.Pow(wave, heso);
            hp = hp * a;
            return hp;
        }
    }
}

