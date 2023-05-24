using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimationEvents : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject throwingGunPrefab;
  public void ThrowGun()
    {
        Instantiate(throwingGunPrefab, transform.position, transform.rotation);
    }
}
