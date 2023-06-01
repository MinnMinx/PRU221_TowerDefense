using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEditor.SceneManagement;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;
using Newtonsoft.Json;
using System.Xml.Linq;
using static UnityEngine.Rendering.DebugUI;

namespace Enemy
{
    public class EnemyManager01 : MonoBehaviour
    {
        /// <summary>
        /// List of all enemy.
        /// </summary>
        [SerializeField]
        private List<GameObject> allEnemy = new List<GameObject>();

        /// <summary>
        /// List of normal enemy.
        /// </summary>
        [SerializeField]
        private List<GameObject> enemies = new List<GameObject>();

        /// <summary>
        /// List of special.
        /// </summary>
        [SerializeField]
        private List<GameObject> special = new List<GameObject>();

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

        private Timer timeSpawn;
        private float time = 0;
        private bool checkTime = true;
        private int numberEnemy = 5;
        private int numberWave = 1;
        private bool loadFromFile = false;
        private double heso = 0.9;

        Queue<SmallWave> largeWave = new Queue<SmallWave>();

        Queue<SmallWave> largeWaveData = new Queue<SmallWave>();

        SmallWave wave = new SmallWave();
        // Start is called before the first frame update
        void Start()
        {
            timeSpawn = gameObject.AddComponent<Timer>();
            timeSpawn.Duration = 1f;
            timeSpawn.Run();
            if (!loadFromFile)
            {
                SpawnWave();
            }            
            wave = largeWave.Dequeue();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                LoadEnemyData();
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
                // địch chết do trụ bắn.
                if (enemy.isDead)
                {
                    spawned.Remove(spawned[i]);
                    GameManager.instance.GainMoney(enemy.Money);
                    GameManager.instance.GainScore(1); // hard-code
                    enemy.OnDespawn();                    
                }
                // địch chạm base
                else if (enemy.DealDamage(basePositon.position))
                {
                    // tru` mau cua player
                    spawned.Remove(spawned[i]);
                    GameManager.instance.TakeDamage(enemy.Atk);
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

                // wave nhỏ hơn 6 chỉ sinh quái thường.
                if (numberWave < 6)
                {
                    for (int j = 0; j < numberEnemy; j++)
                    {
                        smallWave.smallWave.Enqueue(enemyType);
                    }
                }
                // wave lớn hơn sáu mỗi wave sẽ có 1 quái special.
                else
                {
                    for (int j = 0; j < numberEnemy - 1; j++)
                    {
                        smallWave.smallWave.Enqueue(enemyType);
                    }

                    var specialType = special[rnd.Next(special.Count)];
                    smallWave.smallWave.Enqueue(specialType);
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

            // lưu data wave.
            largeWaveData = largeWave;
        }

        public void SaveEnemyData()
        {
            EnemyData data = new EnemyData()
            {
                numberEnemy = this.numberEnemy,
                numberWave = this.numberWave,
                largeWave = new List<List<string>>(),
            };

            foreach (var item in largeWaveData)
            {
                List<GameObject> list = new List<GameObject>();
                list = item.smallWave.ToList();
                data.largeWave.Add(list.Select(enemy => enemy.name).ToList());
            }

            string filePath = "Assets/Resources/EnemyData.json";
            string jsonData = JsonConvert.SerializeObject(data);

            // Ghi dữ liệu vào tệp tin
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(jsonData);
            sw.Close();
        }

        public void LoadEnemyData()
        {
            allEnemy.AddRange(enemies);
            allEnemy.AddRange(special);
            allEnemy.AddRange(bosses);

            string filePath = "Assets/Resources/EnemyData.json";
            string jsonContent = File.ReadAllText(filePath);

            EnemyData data = JsonConvert.DeserializeObject<EnemyData>(jsonContent);
            this.numberEnemy = data.numberEnemy;
            this.numberWave = data.numberWave;
            foreach (var wave in data.largeWave)
            {
                SmallWave smWave = new SmallWave()
                {
                    smallWave = new Queue<GameObject>(),
                };

                foreach(var enemy in wave)
                {
                    var enemyInWave = allEnemy.FirstOrDefault(gameobject => gameobject.name.Equals(enemy));
                    smWave.smallWave.Enqueue(enemyInWave);
                }
                largeWave.Enqueue(smWave);
            }
        }

        private class SmallWave
        {
            public Queue<GameObject> smallWave { get; set; }
        }

        [System.Serializable]
        public class EnemyData
        {
            public int numberEnemy { get; set; }
            public int numberWave { get; set; }
            public List<List<string>> largeWave { get; set;}
        }
    }
}
