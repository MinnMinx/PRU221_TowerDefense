using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner01 : MonoBehaviour
{
    /// <summary>
    /// List of enemy.
    /// </summary>
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();

    /// <summary>
    /// Spawned enemies.
    /// </summary>
    public static List<GameObject> spawned = new List<GameObject>();

    /// <summary>
    /// screen size.
    /// </summary>
    float screenLeft;
    float screenRight;
    float screenTop;
    float screenBottom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SaveScreenSize();

        if (spawned.Count == 0)
        {
            System.Random rnd = new System.Random();
            List<float> x = new List<float>();
            x.Add(UnityEngine.Random.Range(screenLeft - 1, screenLeft));
            x.Add(UnityEngine.Random.Range(screenRight, screenLeft + 1));

            List<float> y = new List<float>();
            y.Add(UnityEngine.Random.Range(screenBottom - 1, screenBottom));
            y.Add(UnityEngine.Random.Range(screenTop, screenTop + 1));

            Vector3 pos = new Vector3(x[rnd.Next(x.Count)], y[rnd.Next(y.Count)], 0);
            Spawn(pos);
        }
    }

    private void Spawn(Vector3 position)
    {
        System.Random rnd = new System.Random();
        var enemySpawn = enemies[rnd.Next(enemies.Count)];
        GameObject monster = Instantiate(enemySpawn);
        var enemy = monster.GetComponent<Enemy01_Base>();
        enemy.Atk = 2;
        enemy.Hp = 10;
        enemy.Speed = 3f;
        enemy.Money = 2;
        monster.transform.position = new Vector3(0, 0, 0);
        spawned.Add(monster);
    }

    /// <summary>
    /// function to take screen size.
    /// </summary>
    private void SaveScreenSize()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(screenWidth, screenHeight, screenZ);
        Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        screenLeft = lowerLeftCornerWorld.x;
        screenRight = upperRightCornerWorld.x;
        screenTop = upperRightCornerWorld.y;
        screenBottom = lowerLeftCornerWorld.y;
    }
}
