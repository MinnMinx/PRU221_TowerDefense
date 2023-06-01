using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

namespace Enemy
{
    public class EnemyManager01 : MonoBehaviour
    {
        /// <summary>
        /// List of enemy.
        /// </summary>
        [SerializeField]
        private List<GameObject> enemies = new List<GameObject>();

        /// <summary>
        /// List of boss.
        /// </summary>
        [SerializeField]
        private List<GameObject> bosses = new List<GameObject>();

        /// <summary>
        /// Spawned enemies.
        /// </summary>
        public static List<GameObject> spawned = new List<GameObject>();

        // vi tri spawn.
        [SerializeField]
        private Transform spawnPositon;

        // vi tri base.
        [SerializeField]
        private Transform basePositon;

        // hard-code spawn position
        private Vector3 v3;

        private Timer timeSpawn = new Timer();
        private float time = 0;
        private bool checkTime = true;
        private int numberEnemy = 5;
        private int numberWave = 1;

        private double heso = 0.9;

        Queue<SmallWave> largeWave = new Queue<SmallWave>();

        SmallWave wave = new SmallWave()
        {
            smallWave = new Queue<GameObject>(),
        };
        // Start is called before the first frame update
        void Start()
        {
            timeSpawn = gameObject.AddComponent<Timer>();
            timeSpawn.Duration = 1f;
            timeSpawn.Run();
            SpawnWave();
            wave = largeWave.Dequeue();
        }

        // Update is called once per frame
        void Update()
        {
            // test
            if (Input.anyKeyDown)
            {
                spawned.Clear();
            }

            // set time nghỉ.
            if (spawned.Count == 0 && wave.smallWave.Count == 0)
            {
                time += Time.deltaTime;
                if (time <= 5)
                {
                    checkTime = false;
                }
                else
                {
                    checkTime = true;
                    time = 0;
                    wave = largeWave.Dequeue();
                    numberWave++;
                }
            }

            // Create wave mới
            if (largeWave.Count == 0)
            {
                SpawnWave();
                // tăng số lượng enemy mỗi wave?
            }
            // Check và xóa quái có máu = 0.
            for (int i = 0; i < spawned.Count; i++)
            {
                var enemy = spawned[i].GetComponent<Enemy01_Base>();
                enemy.OnUpdate();
                if (enemy.isDead)
                {
                    spawned.Remove(spawned[i]);
                    enemy.OnDespawn();
                }
                else if (enemy.DealDamage(basePositon.position)) // hardd-code
                {
                    // tru` mau cua player
                    spawned.Remove(spawned[i]);
                    enemy.OnDespawn();
                }
            }

            // thoi` gian spawn moi~ con quai.
            if (timeSpawn.Finished && checkTime)
            {
                Spawn();
                timeSpawn.Run();
            }
        }

        private void Spawn()
        {
            if (wave.smallWave.Count > 0)
            {
                GameObject enemySpawn = wave.smallWave.Dequeue();
                GameObject enemy = Instantiate(enemySpawn, spawnPositon.position, Quaternion.identity); // hard-code
                var enemyStat = enemy.GetComponent<Enemy01_Base>();
                enemyStat.SetHp(numberWave, heso);
                spawned.Add(enemy);
            }
        }

        private void SpawnWave()
        {
            System.Random rnd = new System.Random();
            for (int i = 0; i < 5; i++)
            {
                var enemyType = enemies[rnd.Next(enemies.Count)];
                SmallWave smallWave = new SmallWave()
                {
                    smallWave = new Queue<GameObject>(),
                };

                for (int j = 0; j < numberEnemy; j++)
                {
                    smallWave.smallWave.Enqueue(enemyType);
                }

                this.largeWave.Enqueue(smallWave);
            };

            var boss = bosses[rnd.Next(bosses.Count)];
            SmallWave bossWave = new SmallWave()
            {
                smallWave = new Queue<GameObject>(),
            };
            bossWave.smallWave.Enqueue(boss);
            this.largeWave.Enqueue(bossWave);
        }

        private class SmallWave
        {
            public Queue<GameObject> smallWave { get; set; }
        }
    }
}
