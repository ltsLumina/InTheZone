using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // configurable parameters
    [SerializeField] float enemySpawnFrequency = 3f;
    [SerializeField] float xSpawnConstraint = 20;
    [SerializeField] float zSpawnConstraint = 20;
    [SerializeField] int initialEnemySpawnCount;
    [SerializeField] GameObject spawnHeightChecker;
    [SerializeField] float heightCheckerHeight = 20;

    
    private void Start()
    {
        // generates the enemies and starts the spawner loop
        GenerateEnemies();
        StartCoroutine(EnemySpawningRoutine());
    }

    private void GenerateEnemies()
    {
        // calls the SpawnEnemy() an equal amount of times to the initialSpawnEnemyCount assigned value
        for (int i = 0; i < initialEnemySpawnCount; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        // spawns a groundchecker on a random coordinate, y value is constant
        Vector3 spawnLocation = new Vector3(Random.Range(-xSpawnConstraint, xSpawnConstraint), heightCheckerHeight, Random.Range(-zSpawnConstraint, zSpawnConstraint));
        Instantiate(spawnHeightChecker, spawnLocation ,Quaternion.identity);
    }

    // loops spawning to keep enemies spawning continuously 
    IEnumerator EnemySpawningRoutine()
    {
        yield return new WaitForSeconds(enemySpawnFrequency);

        SpawnEnemy();
        StartCoroutine(EnemySpawningRoutine());
    }
}
