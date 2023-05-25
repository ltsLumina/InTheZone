using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerGroundcheck : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject[] enemyTypes;

    EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();

        // spawns an enemy once raycast hits ground layer
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, groundLayer))
        {
            Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], hit.point += new Vector3(0, 1, 0), Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
