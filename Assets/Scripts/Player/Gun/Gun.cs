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

    [Header("Bullet")]
    [SerializeField] float bulletForce = 15f;
    [SerializeField] GameObject bulletPrefab;

    [Header("Read-Only Fields")]
    [SerializeField, ReadOnly] bool canFire;

    // Cached References.
    Magazine magazine;
    Camera playerCam;
    ParticleRunner particleRunner;
    GameObject bulletsFired;
    
    // onshoot event
    public delegate void OnShoot();
    public event OnShoot onShoot;

    // Cached Hashes
    readonly static int DoShoot = Animator.StringToHash("doShoot");

    public Animator GunAnim { get; private set; }

    public float ShootDelay
    {
        get => shootDelay;
        set => shootDelay = value;
    }
    public bool CanFire 
    { 
        get => canFire; 
        set => canFire = value; 
    }

    void Start()
    {
        // subscribe to the onShoot event
        onShoot += () =>
        {
            Shoot();
            particleRunner.GunParticles();
        };

        magazine       = GetComponent<Magazine>();
        GunAnim        = FindObjectOfType<GunAnimationEvents>().GetComponent<Animator>();
        playerCam      = FindObjectOfType<Camera>();
        particleRunner = GetComponentInChildren<ParticleRunner>();

        // Create a header for the bullets fired.
        bulletsFired = new GameObject("Bullets Fired");

        // Set the gun to be able to fire.
        CanFire = true;

        // Set the current magazine to the maximum size.
        magazine.CurrentMagCount = magazine.MaxMagazineSize;
    }

    void Update()
    {
        // "Fire1" == Left mouse button.
        if (Input.GetButtonDown("Fire1") && magazine.CurrentMagCount > 0 && CanFire && !magazine.Reloading()) onShoot?.Invoke();

        if (Input.GetKeyDown(KeyCode.R)) Reload();
    }

    void Shoot()
    {
        // Shoot the gun.
        StartCoroutine(Sequencing.SequenceActions(() =>
        {
            CanFire = false;

            GunAnim.SetTrigger(DoShoot);

            magazine.CurrentMagCount--;
            magazine.UpdateAmmoText();

            // instantiate a bullet and add force towards the hit object
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce, ForceMode.Impulse);

            bullet.transform.parent = bulletsFired.transform;
            Destroy(bullet, 3.5f);

            // Raycast to a distance of 100 units and debug the name of the object hit.
            if (!Physics.Raycast
                (playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit, range)) return;

            Debug.Log($"Hit: {hit.transform.name}");

            switch (hit.transform.tag)
            {
                case "Enemy":
                    // Get the health component from the object hit.
                    if (hit.transform.TryGetComponent(out IDamageable component))
                        // Do on-hit logic, such as damaging the enemy etc.
                        component.TakeDamage(damage);
                    break;

                case "DroppedGun":
                    // Add force to the dropped gun in the direction of the raycast
                    Rigidbody droppedGunRB = hit.transform.GetComponent<Rigidbody>();
                    droppedGunRB.AddForce(playerCam.transform.forward * 6, ForceMode.Impulse);
                    droppedGunRB.AddForce(transform.up                * 8, ForceMode.Impulse);
                    droppedGunRB.AddTorque
                    (new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20)),
                     ForceMode.Impulse);
                    break;
            }

            // ShootDelay == the time between each shot.
        }, ShootDelay, () => CanFire = true));
    }

    void Reload()
    {
        magazine.ReloadMagazine();
    }
}