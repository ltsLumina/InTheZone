using UnityEngine;

public class Pickup : MonoBehaviour
{
    Gun gun;

    void Start()
    {
        gun = FindObjectOfType<Gun>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) { return;}
        gun.FireRate *= 1.05f;
        float round = Mathf.Round(gun.Magazine * 1.25f);
        gun.Magazine = round;
        Debug.Log($"FireRate = {gun.FireRate}Magazine = {gun.Magazine}");
        Destroy(gameObject);
    }
}