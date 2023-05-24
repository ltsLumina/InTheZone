using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] float time = 1f;
    [SerializeField] float repeatRate = 1f;
    [SerializeField] GameObject pickUp;
    [SerializeField] float minInclusiveX = 0f;
    [SerializeField] float maxExclusiveX = 10f;
    [SerializeField] float minInclusiveZ = 0f;
    [SerializeField] float maxExclusiveZ = 10f;

    Vector3 spawnPosition;

    void Start()
    {
        InvokeRepeating(nameof(SpawnPickup), time, repeatRate);
    }

    void SpawnPickup()
    {
        //Randomize the spawning position and then instantiate the pickup on the random spot
        spawnPosition = new(Random.Range(minInclusiveX, maxExclusiveX), transform.position.y, Random.Range(minInclusiveZ, maxExclusiveZ));
        Instantiate(pickUp, spawnPosition, Quaternion.identity);
        pickUp.transform.parent = gameObject.transform;
    }
}