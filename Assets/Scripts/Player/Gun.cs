#region
using UnityEngine;
using static Essentials.Attributes;
#endregion

public class Gun : MonoBehaviour
{
    [SerializeField] float range = 100f;
    [SerializeField] int damage = 35;
    [SerializeField] float fireRate = 1.88f;
    [SerializeField] float maxMagazineSize = 18;

    [SerializeField, ReadOnly] float currentMagazine;

    bool allowFire = true;
    Camera playerCam;

    public float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }

    public float MaxMagazineSize
    {
        get => maxMagazineSize;
        set => maxMagazineSize = value;
    }

    void Start()
    {
        // Get the camera component from the child of this object.
        playerCam = transform.parent.GetChild(0).GetComponent<Camera>();

        // Set the current magazine to the maximum size.
        currentMagazine = maxMagazineSize;
    }

    void Update()
    {
        // "Fire1" == Left mouse button.
        if (Input.GetButtonDown("Fire1") && currentMagazine > 0 && allowFire)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        currentMagazine--;

        // Shoots
        StartCoroutine(Essentials.Sequencing.SequenceActions(() =>
        {
            allowFire = false;
            Debug.Log("allowFire = false");

            // Raycast to a distance of 100 units and debug the name of the object hit.
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit, range))
            {
                Debug.Log($"Hit: {hit.transform.name}");

                if (hit.collider != null)
                {
                    // Do on-hit logic, such as damaging the enemy etc.
                    // Get the health component from the object hit.
                    //Health target = hit.transform.GetComponent<Health>();
                }
            }

        }, FireRate, () =>
        {
            // Shoot has finished, perform clean up actions.
            allowFire = true;
            Debug.Log("allowFire = true");
        }));
    }
}