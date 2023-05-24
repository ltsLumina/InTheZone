#region
using UnityEngine;
#endregion

public class Magazine : MonoBehaviour
{
    // Cached Hashes
    readonly static int OnReload = Animator.StringToHash("onReload");
    [SerializeField] float maxMagSize = 18;
    [SerializeField] float currentMagCount;

    // Cached References
    Gun gun;

    public float MaxMagazineSize
    {
        get => maxMagSize;
        set => maxMagSize = value;
    }

    public float CurrentMagSize
    {
        get => currentMagCount;
        set
        {
            currentMagCount = value;

            // Check if magazine is empty, and automatically reload if it is.
            if (currentMagCount <= 0) ReloadMagazine();
        }
    }

    void Start()
    {
        gun = GetComponent<Gun>();

        Debug.Assert(currentMagCount < 0 == false,
                     "CurrentMagSize is less than 0! \n An error has occured somewhere!");
    }

    public void ReloadMagazine()
    {
        gun.GunAnim.SetTrigger(OnReload);
        CurrentMagSize = MaxMagazineSize;
    }
}