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
    [SerializeField] float shootDelay;
    [SerializeField] int damage = 35;

    [Header("Read-Only Fields")]
    [SerializeField, ReadOnly] bool canFire;

    // Cached References.
    Magazine magazine;
    Camera playerCam;

    // Cached Hashes
    readonly static int OnShoot = Animator.StringToHash("onShoot");

    public Animator GunAnim { get; private set; }

    public float ShootDelay
    {
        get => shootDelay;
        set => shootDelay = value;
    }

    void Start()
    {
        canFire  = true;

        magazine  = GetComponent<Magazine>();
        GunAnim   = FindObjectOfType<GunAnimationEvents>().GetComponent<Animator>();
        playerCam = FindObjectOfType<Camera>();

        // Set the current magazine to the maximum size.
        magazine.CurrentMagSize = magazine.MaxMagazineSize;
    }

    void Update()
    {
        // "Fire1" == Left mouse button.
        if (Input.GetButton("Fire1") && magazine.CurrentMagSize > 0 && canFire) Shoot();

        if (Input.GetKeyDown(KeyCode.R)) Reload();
    }

    void Shoot()
    {
        // Shoot the gun.
        StartCoroutine(Sequencing.SequenceActions(() =>
        {
            canFire = false;
            magazine.CurrentMagSize--;
            GunAnim.SetTrigger(OnShoot);

            // Raycast to a distance of 100 units and debug the name of the object hit.
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit, range))
            {
                Debug.Log($"Hit: {hit.transform.name}");

                // Get the health component from the object hit.
                if (hit.transform.TryGetComponent(out IDamageable component))
                    // Do on-hit logic, such as damaging the enemy etc.
                    component.TakeDamage(damage);
            }

            // ShootDelay == the time between each shot.
        }, ShootDelay, () =>
        {
            // Shoot has finished, perform clean up actions.
            canFire = true;
        }));
    }

    void Reload()
    {
        magazine.ReloadMagazine();
    }
}