using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

namespace Enemy
{
    public class EnemyManager01 : MonoBehaviour
    {
        /// <summary>
        /// List of all enemy.
        /// </summary>
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
        public List<GameObject> spawned = new List<GameObject>();

        // vi tri spawn.
        [SerializeField]
        private Transform spawnPositon;

        // audio enemy die
        AudioSource enemyDead;

        // vi tri base.
        [SerializeField]
        private Transform basePositon;

        private Timer timeSpawn;
        private float time = 0;
        private bool gainEnemy = true;
        private bool checkTime = true;
        private int numberEnemy = 4;
        private int numberWave = 1;
        private double heso = 1;

        Queue<SmallWave> largeWave = new Queue<SmallWave>();

        SmallWave wave = new SmallWave();

        public static string SAVE_ENEMY_EVT = "SAVE_ENEMY_EVT";
        public static string LOAD_ENEMY_EVT = "LOAD_ENEMY_EVT";

        // Start is called before the first frame update
        void Start()
        {
            enemyDead = gameObject.GetComponent<AudioSource>();
            timeSpawn = gameObject.AddComponent<Timer>();
            timeSpawn.Duration = 1f;
            timeSpawn.Run();
            SpawnWave();
            wave = largeWave.Dequeue();

            GameUiEventManager.Instance.RegisterEvent(SAVE_ENEMY_EVT, (s, o) => SaveEnemyData());
            GameUiEventManager.Instance.RegisterEvent(LOAD_ENEMY_EVT, (s, o) => LoadEnemyData());
        }

        // Update is called once per frame
        void Update()
        {
            // set time nghỉ.
            if (spawned.Count == 0 && wave.smallWave.Count == 0)
            {
                time += Time.deltaTime;
                if (time <= 1)
                {
                    checkTime = false;
                }
                else
                {
                    checkTime = true;
                    time = 0;
                    wave = largeWave.Dequeue();

                    if (numberWave % 6 == 0 && !gainEnemy)
                    {
                        gainEnemy = true;
                    }

                    numberWave++;
                }
            }

            // mỗi khi hết 6 wave tăng 3 quái.
            if (numberWave % 6 == 0 && gainEnemy)
            {
                numberEnemy += 3;
                gainEnemy = false;
            }


            // Create wave mới
            if (largeWave.Count == 0)
            {
                SpawnWave();
            }

            float deltaTime = Time.deltaTime;
            // Check và xóa quái có máu = 0.
            for (int i = 0; i < spawned.Count; i++)
            {
                var enemy = spawned[i].GetComponent<Enemy01_Base>();
                enemy.OnUpdate(deltaTime);
                // địch chết do trụ bắn.
                if (enemy.isDead)
                {
                    spawned.Remove(spawned[i]);
                    GameManager.instance.GainMoney(enemy.Money);
                    
                    // tăng điểm bằng tiền x wave.
                    GameManager.instance.GainScore(enemy.Money * numberWave);
                    enemy.OnDespawn();
                    enemyDead.Play();
                    enemyDead.volume = 0.25f;
                }
                // địch chạm base
                else if (enemy.DealDamage(basePositon.position))
                {
                    // tru` mau cua player
                    spawned.Remove(spawned[i]);
                    GameManager.instance.TakeDamage(enemy.Atk);
                    enemy.OnDespawn();
                    enemyDead.Play();
                    enemyDead.volume = 0.25f;
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
                GameObject enemy = Instantiate(enemySpawn, spawnPositon.position, Quaternion.identity);
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
                SmallWave smallWave = new SmallWave();

                // wave nhỏ hơn 6 chỉ sinh quái thường.
                if (numberWave < 6)
                {
                    for (int j = 0; j < numberEnemy; j++)
                    {
                        var enemyType = enemies[rnd.Next(enemies.Count)];
                        smallWave.smallWave.Enqueue(enemyType);
                    }
                }
                // wave lớn hơn sáu mỗi wave sẽ có 1 quái special.
                else
                {
                    for (int j = 0; j < numberEnemy - 1; j++)
                    {
                        var enemyType = enemies[rnd.Next(enemies.Count)];
                        smallWave.smallWave.Enqueue(enemyType);
                    }

                    var specialType = special[rnd.Next(special.Count)];
                    smallWave.smallWave.Enqueue(specialType);
                }
                

                this.largeWave.Enqueue(smallWave);
            };

            var boss = bosses[rnd.Next(bosses.Count)];
            SmallWave bossWave = new SmallWave();

            bossWave.smallWave.Enqueue(boss);
            this.largeWave.Enqueue(bossWave);

            //SaveEnemyData();
        }

        public void SaveEnemyData()
        {
            //1 7 13 19 
            EnemyData data = new EnemyData()
            {
                numberEnemySaved = this.numberEnemy,
                numberWave = this.numberWave,
                largeWave = new List<List<string>>(),
            };

            if (!wave.smallWave.Select(x => x.name).Contains("Boss")
                && wave.smallWave.Count > 0
                && wave.smallWave.Count < numberEnemy)
            {
                List<string> string1 = new List<string>();
                string1.AddRange(spawned.Select(x => x.name));
                foreach (var item in wave.smallWave)
                {
                    string1.Add(item.name);
                }
                data.largeWave.Add(string1);
            }
            else
            {
                List<string> string1 = new List<string>();
                string1.AddRange(spawned.Select(x => x.name));
                data.largeWave.Add(string1);
            }

            foreach (var item in largeWave)
            {                
                List<GameObject> list2 = new List<GameObject>();
                list2 = item.smallWave.ToList();
                data.largeWave.Add(list2.Select(enemy => enemy.name).ToList());
            }

            string filePath = Application.streamingAssetsPath + "/EnemyData.json";
            string jsonData = JsonConvert.SerializeObject(data);

            // Ghi dữ liệu vào tệp tin
            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(jsonData);
            sw.Close();
        }

        public void LoadEnemyData()
        {
            try
            {
                allEnemy.AddRange(enemies);
                allEnemy.AddRange(special);
                allEnemy.AddRange(bosses);

                string filePath = Application.streamingAssetsPath + "/EnemyData.json";
                string jsonContent = File.ReadAllText(filePath);
                largeWave.Clear();
                spawned.ForEach(x => Destroy(x));
                spawned.Clear();
                EnemyData data = JsonConvert.DeserializeObject<EnemyData>(jsonContent);
                this.numberWave = data.numberWave;
                this.numberEnemy = data.numberEnemySaved;
                foreach (var wave in data.largeWave)
                {
                    SmallWave smWave = new SmallWave();

                    foreach (var enemy in wave)
                    {
                        var enemyInWave = allEnemy.FirstOrDefault(gameobject => gameobject.name.Equals(enemy.Replace("(Clone)", "")));
                        smWave.smallWave.Enqueue(enemyInWave);
                    }
                    largeWave.Enqueue(smWave);
                }

                wave = largeWave.Dequeue();
            }
            catch
            {
                SpawnWave();
            }
            
        }

        private class SmallWave
        {
            public SmallWave()
            {
                smallWave = new Queue<GameObject>();
            }
            public Queue<GameObject> smallWave { get; set; }
        }

        [System.Serializable]
        public class EnemyData
        {
            public int numberEnemySaved { get; set; }
            public int numberWave { get; set; }
            public List<List<string>> largeWave { get; set;}
        }
    }
}
