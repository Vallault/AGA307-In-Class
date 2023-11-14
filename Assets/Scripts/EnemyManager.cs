using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    OneHand, 
    TwoHand, 
    Archer
}

public enum PatrolType
{
    Linear, Random, Loop
}

public class EnemyManager : Singleton<EnemyManager>
{
    public Transform[] spawnPoints;
    public string[] enemyNames;
    public GameObject[] enemyTypes;

    public List<GameObject> enemies;
    public string killCondition = "Two";

    void Start()
    {
        //SpawnEnemies();
        //SpawnAtRandom();
        StartCoroutine(SpawnEnemiesWithDelay());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            SpawnAtRandom();

        if (Input.GetKeyDown(KeyCode.K))
            KillAllEnemies();

        if (Input.GetKeyDown(KeyCode.P))
            KillSpecificEnemies(killCondition);
    }

    /// <summary>
    /// Spawns an ememy every random amount of seconds
    /// </summary>
    IEnumerator SpawnEnemiesWithDelay()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            int rnd = Random.Range(0, enemyTypes.Length);
            GameObject enemy = Instantiate(enemyTypes[rnd], spawnPoints[i].position, spawnPoints[i].rotation);
            enemies.Add(enemy);
            SetEnemyName(enemy);
            ShowEnemyCount();
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }

    /// <summary>
    /// Spawns an enemy at every spawn point
    /// </summary>
    void SpawnEnemies()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            int rnd = Random.Range(0, enemyTypes.Length);
            GameObject enemy = Instantiate(enemyTypes[rnd], spawnPoints[i].position, spawnPoints[i].rotation);
            enemies.Add(enemy);
            SetEnemyName(enemy);
        }
        ShowEnemyCount();
    }

    /// <summary>
    /// Spawns a random enemy at a random spawn point
    /// </summary>
    public void SpawnAtRandom()
    {
        int rndEnemy = Random.Range(0, enemyTypes.Length);
        int rndSpawn = Random.Range(0, spawnPoints.Length);
        GameObject enemy = Instantiate(enemyTypes[rndEnemy], spawnPoints[rndSpawn].position, spawnPoints[rndSpawn].rotation);
        enemies.Add(enemy);
        SetEnemyName(enemy);
        ShowEnemyCount();
    }

    /// <summary>
    /// Shows the amount of enemies in the stage
    /// </summary>
    void ShowEnemyCount()
    {
        _UI.UpdateEnemyCount(enemies.Count);
    }

    /// <summary>
    /// Sets the enemy name
    /// </summary>
    /// <param name="_enemy">The enemy name to set</param>
    void SetEnemyName(GameObject _enemy)
    {
        //_enemy.GetComponent<Enemy>().SetName(enemyNames[Random.Range(0, enemyNames.Length)]);
    }

    /// <summary>
    /// Gets an enemy name
    /// </summary>
    /// <returns></returns>
    public string GetEnemyName()
    {
        return enemyNames[Random.Range(0, enemyNames.Length)];
    }


    /// <summary>
    /// Kills a specific enemy
    /// </summary>
    /// <param name="_enemy">The enemy we want to kill</param>
    public void KillEnemy(GameObject _enemy)
    {
        if (enemies.Count == 0)
            return;

        //Destroy(_enemy, 5);
        enemies.Remove(_enemy);
        ShowEnemyCount();
    }

    /// <summary>
    /// Kills all enemies in our stage
    /// </summary>
    void KillAllEnemies()
    {
        if (enemies.Count == 0)
            return;

        for(int i = enemies.Count-1; i >= 0; i--)
        {
            KillEnemy(enemies[i]);
        }
    }

    /// <summary>
    /// Kills specific enemies
    /// </summary>
    /// <param name="_condition">The condition of the enemy we want to kill</param>
    void KillSpecificEnemies(string _condition)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].name.Contains(_condition))
                KillEnemy(enemies[i]);
        }
    }

    /// <summary>
    /// Get a random spawn point
    /// </summary>
    /// <returns>A random spawn point</returns>
    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDie += KillEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDie -= KillEnemy;
    }

    void Examples()
    {
        int numberRepetitions = 2000;
        for (int i = 0; i <= numberRepetitions; i++)
        {
            print(i);
        }

        GameObject first = Instantiate(enemyTypes[0], spawnPoints[0].position, spawnPoints[0].rotation);
        first.name = enemyNames[0];

        int lastEnemy = enemyTypes.Length - 1;
        GameObject last = Instantiate(enemyTypes[lastEnemy], spawnPoints[lastEnemy].position, spawnPoints[lastEnemy].rotation);
        last.name = enemyNames[lastEnemy];

        //Create a loop within a loop for a wall
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Instantiate(wall, new Vector3(i, j, 0), transform.rotation);
            }
        }
    }
}
