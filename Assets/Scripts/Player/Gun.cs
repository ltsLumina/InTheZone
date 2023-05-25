#region
using Essentials;
using UnityEngine;
using static Essentials.Attributes;
using static Interfaces;
#endregion

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] float range = 100f;
    [SerializeField] float fireRate;
    [SerializeField] int damage = 35;

    [Header("Read-Only Fields")]
    [SerializeField, ReadOnly] bool canFire;

    // Cached References.
    Magazine magazine;
    Camera playerCam;

    public float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }

    void Start()
    {
        canFire  = true;
        magazine = GetComponent<Magazine>();

        // Set the current magazine to the maximum size.
        magazine.CurrentMagSize = magazine.MaxMagazineSize;

        // Get the camera component from the child of this object.
        playerCam = transform.parent.GetChild(0).GetComponent<Camera>();
    }

    void Update()
    {
        // "Fire1" == Left mouse button.
        if (Input.GetButton("Fire1") && magazine.CurrentMagSize > 0 && canFire) Shoot();
    }

    void Shoot()
    {
        magazine.CurrentMagSize--;

        // Shoots
        StartCoroutine(Sequencing.SequenceActions(() =>
        {
            canFire = false;

            // Raycast to a distance of 100 units and debug the name of the object hit.
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit, range))
            {
                Debug.Log($"Hit: {hit.transform.name}");

                // Get the health component from the object hit.
                if (hit.transform.TryGetComponent(out IDamageable component))
                    // Do on-hit logic, such as damaging the enemy etc.
                    component.TakeDamage(damage);
            }

            // FireRate == the time between each shot.
        }, FireRate, () =>
        {
            // Shoot has finished, perform clean up actions.
            canFire = true;
        }));
    }
}