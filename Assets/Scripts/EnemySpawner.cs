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
    [SerializeField] int enemySpawnCount;
    [SerializeField] GameObject heightChecker;
    [SerializeField] float heightCheckerSpawnHeight = 20f;
    [SerializeField] bool autoSpawnEnemies;
    [SerializeField] bool randomizeEnemyTypes;
    [SerializeField] float enemyMultiplier = 1.25f;
    [SerializeField] float enemyMultiplierTimer = 60f;

    // private variables
    Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        // generates the enemies and starts the spawner loop if it is enabled in the editor
        GenerateEnemies(enemySpawnCount);
        StartCoroutine(EnemySpawningRoutine());     
        StartCoroutine(EnemyMultiplierRoutine());
    }

    public void GenerateEnemies(int enemyAmount)
    {
        Debug.Log(enemyAmount);
        // calls the SpawnEnemy() an equal amount of times to the inputted enemyAmount value
        for (int i = 0; i < enemyAmount; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnDirection = Quaternion.AngleAxis(Random.Range(playerVisionAngle, 360 - playerVisionAngle), Vector3.down) * player.forward;
        Vector3 spawnPosition = player.position + (spawnDirection * Random.Range(minEnemySpawnDistance, maxEnemySpawnDistance)) + new Vector3(0, heightCheckerSpawnHeight, 0);

        Instantiate(heightChecker, spawnPosition, Quaternion.identity);
    }

    // loops the coroutine to keep enemies spawning continuously 
    private IEnumerator EnemySpawningRoutine()
    {
        yield return new WaitForSeconds(enemySpawnFrequency);

        GenerateEnemies(enemySpawnCount);
        StartCoroutine(EnemySpawningRoutine());
    }

    private IEnumerator EnemyMultiplierRoutine()
    {
        yield return new WaitForSeconds(enemyMultiplierTimer);

        enemySpawnCount = (int)Mathf.Round(enemySpawnCount * enemyMultiplier);
        StartCoroutine(EnemyMultiplierRoutine());
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
