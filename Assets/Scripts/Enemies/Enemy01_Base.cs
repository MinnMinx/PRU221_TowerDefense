using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;
using static Enemy.Modifier;

namespace Enemy
{
    public class Enemy01_Base : MonoBehaviour
    {
        private Color slow;       
        private SpriteRenderer sprite;

        public GameObject Explosion;
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
            set
            {
                speed = value;
                speedBase = value;
            }
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

        // check theo máu quái
        public bool isDead => hp <= 0;

        // tốc độ trước khi bị làm chậm của quái.
        public float speedBase;

        // check xem quái có miễn làm trận.
        public bool canSpeed = false;

        // List buff/debuff của quái
        public List<Modifier> modifiers = new List<Modifier>();
        
        // thanh máu của quái
        HealthBarBehaviour healthBar;
        
        // A* PathFinding
        AIPath followPathAI;

        // flag quay quái
        private bool checkflip = true;

        protected virtual void Awake()
        {
            ColorUtility.TryParseHtmlString("#" + "4E73FF", out slow);
            maxHp = hp;
            healthBar = GetComponentInChildren<HealthBarBehaviour>();
            followPathAI = GetComponent<AIPath>();
            // set tốc độ cho quái
            followPathAI.maxSpeed = speed;
        }

        public virtual bool OnUpdate(float deltaTime)
        {
            sprite = gameObject.GetComponent<SpriteRenderer>();

            // giảm time modifier.
            if (modifiers.Count > 0)
            {
                foreach (var item in modifiers)
                {
                    item.timeLeft -= deltaTime;
                }
            }

            // xóa modifier
            RemoveExpiredModifiers();

            // tính lại stat
            CalculatorModifier();

            // quay qúai khi di chuyển
            RotationEnemy(deltaTime);
            return isDead;
        }

        private void RotationEnemy(float deltaTime)
        {
            // lấy điểm tiếp theo quái sẽ di chuyển
            followPathAI.MovementUpdate(deltaTime, out Vector3 pos, out Quaternion rot);
            // check vị trí hiện tại vs ví trị tiếp theo 
            float distanceX = transform.position.x - pos.x;
            if ((distanceX < 0 && !checkflip) || (distanceX > 0 && checkflip))
            {
                // quay quái
                checkflip = !checkflip;
                Vector3 scale = transform.localScale;
                scale.x = -scale.x;
                transform.localScale = scale;

                // giữ nguyên thanh máu
                Vector3 scale1 = gameObject.GetComponentInChildren<Slider>().transform.localScale;
                scale1.x = -scale1.x;
                gameObject.GetComponentInChildren<Slider>().transform.localScale = scale1;
            }
        }

        /// <summary>
        /// Destroy enemy.
        /// </summary>
        public virtual void OnDespawn()
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        /// <summary>
        /// deal damage to enemy.
        /// </summary>
        /// <param name="damage"></param>
        public virtual void TakeDamage(decimal damage)
        {
            hp = hp - damage;
            healthBar.SetHealth(Convert.ToSingle(hp/maxHp));
        }

        public bool DealDamage(Vector3 vector3)
        {
            float distance = Vector3.Distance(gameObject.transform.position, vector3);
            if (distance <= 0.2f) 
                return true;
            return false;
        }

        // set hp theo wave cho enemy
        public decimal SetHp(int wave, double heso)
        {
            decimal a = (decimal)Math.Pow(wave, heso);
            maxHp = maxHp * a;
            hp = maxHp;
            healthBar.SetHealth(Convert.ToSingle(hp/maxHp));
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
            // speed
            var spdModifiers = modifiers.Where(modifier => modifier.type == ModifierType.Spd).OrderByDescending(modifier => modifier.multipler).FirstOrDefault();

            if (spdModifiers != null && spdModifiers.timeLeft <= 0)
            {
                //set lại speed.
                speed = speedBase;
            }

            // xóa modifer có timeleft <= 0.
            modifiers.RemoveAll(modifier => modifier.timeLeft <= 0);
        }

        public void CalculatorModifier()
        {
            // modifier speed.
            var spdModifiers = modifiers.Where(modifier => modifier.type == ModifierType.Spd).OrderByDescending(modifier => modifier.multipler).FirstOrDefault();

            if (spdModifiers != null && !canSpeed)
            {
                sprite.color = slow;
                speed = speedBase * (1 - spdModifiers.multipler);
            }
            else
            {
                sprite.color = Color.white;
            }
        }
    }
}

