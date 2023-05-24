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
}