using System;
using System.Security.Cryptography;
using UnityEngine;

public class Gun : MonoBehaviour
{
  [SerializeField] Camera FPCamera;
  [SerializeField] float range = 100f;
  [SerializeField] int damage = 35;

  void Update()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      Shoot();
    }
  }

  void Shoot()
  {
    RaycastHit hit;

    if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
    {
      Debug.Log("I hit this thing" + hit.transform.name);
    }
  }
}