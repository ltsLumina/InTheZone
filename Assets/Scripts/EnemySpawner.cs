using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // configurable parameters
    [SerializeField] float enemySpawnFrequency = 3f;
    [SerializeField] float minEnemySpawnDistance = 15f;
    [SerializeField] float maxEnemySpawnDistance = 60f;
    [SerializeField] float playerVisionAngle = 50f;
    [SerializeField] int initialEnemySpawnCount;
    [SerializeField] GameObject spawnHeightChecker;
    [SerializeField] float heightCheckerHeight = 20f;
    [SerializeField] bool autoSpawnEnemies;

    // private variables
    Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        // generates the enemies and starts the spawner loop
        GenerateEnemies(initialEnemySpawnCount);

        if (autoSpawnEnemies)
        {
            StartCoroutine(EnemySpawningRoutine());
        }
    }

    public void GenerateEnemies(int enemyAmount)
    {
        // calls the SpawnEnemy() an equal amount of times to the inputted enemyAmount value
        for (int i = 0; i < enemyAmount; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Vector3 spawnDirection = Quaternion.AngleAxis(Random.Range(playerVisionAngle, 360 - playerVisionAngle), Vector3.down) * player.forward;
        Vector3 spawnPosition = player.position + (spawnDirection * Random.Range(minEnemySpawnDistance, maxEnemySpawnDistance)) + new Vector3(0, heightCheckerHeight, 0);

        Instantiate(spawnHeightChecker, spawnPosition, Quaternion.identity);
    }

    // loops the coroutine to keep enemies spawning continuously 
    public IEnumerator EnemySpawningRoutine()
    {
        yield return new WaitForSeconds(enemySpawnFrequency);

        SpawnEnemy();
        StartCoroutine(EnemySpawningRoutine());
    }

    private void OnDrawGizmosSelected()
    {
        player = GameObject.FindWithTag("Player").transform;

        Gizmos.color = Color.red;

        Vector3 previousPoint = Quaternion.AngleAxis(playerVisionAngle, Vector3.down) * player.forward;

        for (float angle = playerVisionAngle + 5f; angle < 360f - playerVisionAngle; angle += 5f)
        {
            Vector3 nextPoint = Quaternion.AngleAxis(angle, Vector3.down) * player.forward;
            Gizmos.DrawLine(player.position + previousPoint * minEnemySpawnDistance, player.position + nextPoint * minEnemySpawnDistance);
            previousPoint = nextPoint;
        }

        for (float angle = playerVisionAngle + 5f; angle < 360f - playerVisionAngle; angle += 5f)
        {
            Vector3 nextPoint = Quaternion.AngleAxis(angle, Vector3.down) * player.forward;
            Gizmos.DrawLine(player.position + previousPoint * maxEnemySpawnDistance, player.position + nextPoint * maxEnemySpawnDistance);
            previousPoint = nextPoint;
        }
    }
}
