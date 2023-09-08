using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager Instance;
    public static GameManager GetInstance() {
        Init();
        return Instance; 
    }

    float spawnTime;
    bool spawnReady;
    public GameObject[] Enemies = new GameObject[3];
    public GameObject[] SpawnPoint = new GameObject[5];

    static void Init()
    {
        if(Instance == null)
        {
            GameObject manager = GameObject.Find("@Manager");
            if (manager == null) 
            {
                manager = new GameObject { name = "@Manager" };
                manager.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(manager);
            Instance = manager.GetComponent<GameManager>();
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        spawnReady = true;
        Init();
    }

    IEnumerator spawnEnemies()
    {
        spawnReady = false;
        yield return new WaitForSeconds(3f);
        spawnReady = true;
    }

    void EnemySpawn()
    {
        if (spawnReady)
        {
            int ranEnemy = Random.Range(0, 3);
            int ranSpawn = Random.Range(0, 5);
            GameObject enemy = Instantiate(Enemies[ranEnemy], SpawnPoint[ranSpawn].transform.position, SpawnPoint[ranSpawn].transform.rotation);
            Rigidbody2D rb2 = enemy.GetComponent<Rigidbody2D>();
            EnemyController enemyLogic = enemy.GetComponent<EnemyController>();
            if(ranSpawn == 3) 
            { 
                rb2.velocity = new Vector2(enemyLogic.stats.Speed * (-1), -1);
            }
            else if (ranSpawn == 4)
            {
                rb2.velocity = new Vector2(enemyLogic.stats.Speed, -1);
            }
            else
            {
                rb2.velocity = new Vector2(0, enemyLogic.stats.Speed * (-1));
            }
            StartCoroutine(spawnEnemies());
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpawn();
    }
}
