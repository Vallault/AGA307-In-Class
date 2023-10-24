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


public class EnemyManager : MonoBehaviour
{
    public Transform[] spawnPoint;
    public string[] enemyNames;
    public GameObject[] enemyTypes;

    public List<GameObject> enemies;
    public string killCondition = "Two";


    private void Start()
    {
        //SpawnEnemies();
        SpawnAtRandom();
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

    IEnumerator SpawnEnemiesWithDelay()
    {

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            int rnd = Random.Range(0, enemyTypes.Length);
            GameObject enemy = Instantiate(enemyTypes[rnd], spawnPoint[i].position, spawnPoint[i].rotation);
            yield return new WaitForSeconds(2);
        }

    }


    /// <summary>
    /// Spawns an enemy at every spawn point.
    /// </summary>
    void SpawnEnemies()
    {
        int rnd = Random.Range(0, enemyTypes.Length);

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            GameObject enemy = Instantiate(enemyTypes[rnd], spawnPoint[i].position, spawnPoint[i].rotation);
        }
    }


    /// <summary>
    /// Spawn a random enemy at a random spawn point.
    /// </summary>
    void SpawnAtRandom()
    {
        int rndEnemy = Random.Range(0, enemyTypes.Length);
        int rndSpawn = Random.Range(0, spawnPoint.Length);
        GameObject enemy = Instantiate(enemyTypes[rndEnemy], spawnPoint[rndSpawn].position, spawnPoint[rndSpawn].rotation);
        enemies.Add(enemy);
        ShowEnemyCount();
    }


    /// <summary>
    /// Shows the amount of enemies in the stage.
    /// </summary>
    void ShowEnemyCount()
    {

        print("Number of Enemies" + enemies.Count);
    }


    /// <summary>
    /// Kills a specific enemy.
    /// </summary>
    /// <param name="_enemy">The enemy we want to kill<param>
    void KillEnemy(GameObject _enemy)
    {
        if (enemies.Count == 0)
            return;

        Destroy(_enemy);
        enemies.Remove(_enemy);
        ShowEnemyCount();

    }


    /// <summary>
    /// Kills all enemies in our stage.
    /// </summary>
    void KillAllEnemies()
    {
        if (enemies.Count == 0)
            return;

        for(int i= enemies.Count-1; i >= 0; i--)
        {
            KillEnemy(enemies[i]);
        }    
    }


    /// <summary>
    /// Kill specific enemies.
    /// </summary>
    /// <param name="_condition">The condiition of the enemy we want to kill<param>
    void KillSpecificEnemies(string _condition)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].name.Contains(_condition))
                KillEnemy(enemies[i]);
        }
    }

    /// <summary>
    /// Get a random spawn point.
    /// </summary>
    /// <returns></returns>
    public Transform GetRandomSpawnPoint()
    {
        return spawnPoint[Random.Range(0, spawnPoint.Length)];
    }

    void Exmaples()
    {
        //int numberReputations = 2000;

        //GameObject firstEnemy = Instantiate(enemyTypes[0], spawnPoint[0]);
        //firstEnemy.name = enemyNames[0];

        SpawnEnemies();

        //Create a loop within a loop for a wall.
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Instantiate(wall, new Vector3(i, j, 0), transform.rotation);
            }
        }

        //
        for (int i = 0; i <= 100; i++)
        {
            print(i);
        }

        enemyNames[2] = "A new name";
        print(enemyNames[enemyNames.Length - 1]);

        GameObject first = Instantiate(enemyTypes[0], spawnPoint[0].position, spawnPoint[0].rotation);
        first.name = enemyNames[0];

        int lastEnemy = enemyTypes.Length - 1;
        GameObject last = Instantiate(enemyTypes[lastEnemy], spawnPoint[lastEnemy].position, spawnPoint[lastEnemy].rotation);
        last.name = enemyNames[lastEnemy];

    }


}
