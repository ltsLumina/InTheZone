using UnityEngine;
using static Essentials.Attributes;

public class Magazine : MonoBehaviour
{
    [SerializeField] float maxMagSize = 18;
    [SerializeField, ReadOnly] float currentMagSize;

    public float MaxMagazineSize
    {
        get => maxMagSize;
        set => maxMagSize = value;
    }

    public float CurrentMagSize
    {
        get => currentMagSize;
        set
        {
            currentMagSize = value;

            if (currentMagSize <= 0)
            {
                Debug.Log("Out of ammo! (Can't fire) \n Reload to continue firing.");
            }
        }
    }
}