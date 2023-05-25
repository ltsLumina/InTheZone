using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSpawnManager : MonoBehaviour
{
    [Header("Zone Variables")]
    [SerializeField] GameObject zone;
    [SerializeField] GameObject[] zoneSpawnPos;
    [SerializeField] float zoneDuration = 60f;


    private List<GameObject> zoneList = new List<GameObject>();
    int currentZoneIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnFirstZone();
    }
    void SpawnFirstZone()
    {
        GameObject zone1 = InstantiateZone();
        StartCoroutine(DestroyZoneAfterDelay(zone1, 20f));//70 
        StartCoroutine(SpawnZoneWithDelay(10f));//60
    }

    GameObject InstantiateZone()
    {
        int lastZoneIndex = currentZoneIndex;
        do
        {
            currentZoneIndex = Random.Range(0, zoneSpawnPos.Length);
        } while (currentZoneIndex == lastZoneIndex);

        GameObject newZone = Instantiate(zone, zoneSpawnPos[currentZoneIndex].transform.position, Quaternion.identity);
        zoneList.Add(newZone);
        return newZone;
    }

    IEnumerator DestroyZoneAfterDelay(GameObject zoneObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Remove the zone from the array
        zoneList.Remove(zoneObject);

        // Destroy the zone object
        Destroy(zoneObject);
    }

    IEnumerator SpawnZoneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Instantiate the next zone
        GameObject nextZone = InstantiateZone();
        StartCoroutine(DestroyZoneAfterDelay(nextZone, zoneDuration));

        // Start spawning the following zone
        StartCoroutine(SpawnZoneWithDelay(zoneDuration));
    }

}
