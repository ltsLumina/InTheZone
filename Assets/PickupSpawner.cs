using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] float time = 1f;
    [SerializeField] float repeatRate = 1f;
    [SerializeField] GameObject pickUp;

    void Start()
    {
        InvokeRepeating(nameof(SpawnPickup), time, repeatRate);
    }

    void SpawnPickup()
    {
        Instantiate(pickUp, transform.position, Quaternion.identity);
        pickUp.transform.parent = gameObject.transform;
    }
}