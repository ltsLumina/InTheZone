using UnityEngine;

public class Pickup : MonoBehaviour
{
    Gun gun;
    Magazine magazine;

    void Start()
    {
        magazine = FindObjectOfType<Magazine>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        gun.ShootDelay *= 1.05f;
        float round = Mathf.Round(magazine.MaxMagazineSize * 1.25f);
        magazine.MaxMagazineSize = round;
        Debug.Log($"ShootDelay = {gun.ShootDelay}MagazineSize = {magazine.MaxMagazineSize}");
        Destroy(gameObject);
    }
}