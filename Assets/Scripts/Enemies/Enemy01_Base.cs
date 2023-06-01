﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.UI;
using static Enemy.Modifier;

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

        private decimal maxHp;
        public decimal MaxHp
        {
            get { return maxHp; }
            set { maxHp = value; }
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

        public bool isSpeed = false;

        public List<Modifier> modifiers = new List<Modifier>();

        HealthBarBehaviour healthBar;

        protected virtual void Awake()
        {
            maxHp = hp;
            healthBar = GetComponentInChildren<HealthBarBehaviour>();
            
        }

        // Start is called before the first frame 
         // Update is called once per frame
        void Update()
        {
            // giảm time modifier.
            if (modifiers.Count > 0)
            {
                foreach (var item in modifiers)
                {
                    item.timeLeft -= Time.deltaTime;
                }
            }

            Debug.Log("" + hp);
            // xóa modifier
            RemoveExpiredModifiers();

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
            healthBar.SetHealth(Convert.ToSingle(hp), Convert.ToSingle(maxHp));
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
            maxHp = maxHp * a;
            hp = maxHp;
            healthBar.SetHealth(Convert.ToSingle(hp), Convert.ToSingle(maxHp));
            return maxHp;
        }

        // add thêm modifier
        public void AddModifier(Modifier modifier)
        {
            modifiers.Add(modifier);
        }

        // xóa modifier có timeleft <= 0.
        public void RemoveExpiredModifiers()
        {
            // set lại speed.
            foreach (var item in modifiers)
            {
                if (item.timeLeft <= 0)
                {
                    switch (item.type)
                    {
                        case ModifierType.Spd:
                            {
                                // set spped.
                                break;
                            };
                        case ModifierType.Heal:
                            {
                                // set heal.
                                break;
                            };
                        case ModifierType.Dmg_Multipler:
                            {
                                // set dmg.
                                break;
                            }
                    }
                }
            }

            // xóa modifer có timeleft <= 0.
            modifiers.RemoveAll(modifier => modifier.timeLeft <= 0);
        }

        public void CalculatorModifier()
        {
            // modifier speed.
            var a = modifiers.Where(modifier => modifier.type == ModifierType.Spd)
                .Max(modifier => modifier.multipler);
            speed = speed * (1 - a);
        }
    }
}

