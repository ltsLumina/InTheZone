#region
using UnityEngine;
#endregion

public class Gun : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] int damage = 35;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shoot();
    }

    void Shoot()
    {
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out RaycastHit hit, range))
            Debug.Log($"Gun Raycast Hit: {hit.transform.name}");
    }
}