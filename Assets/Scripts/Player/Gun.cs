#region
using System.Collections;
using UnityEngine;
#endregion

public class Gun : MonoBehaviour
{
  [SerializeField] Camera FPCamera;
  [SerializeField] float range = 100f;
  [SerializeField] int damage = 35;
  float magazine;
  [SerializeField] float maxMagazine = 18;
  [SerializeField] float fireRate = 1.88f;

  bool allowFire = true;

  public float FireRate
  {
  get => fireRate;
    set => fireRate = value;
  }

  public float Magazine
  {
    get => maxMagazine;
    set => maxMagazine = value;
  }

  void Start()
  {
    magazine = maxMagazine;
  }

  void Update()
  {
    if (Input.GetButtonDown("Fire1") && magazine > 0 && allowFire)
    {
      StartCoroutine(Shoot());
    }
    
  IEnumerator Shoot()
  {
    magazine--;
    RaycastHit hit;
    if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))

    allowFire = false;
    yield return new WaitForSeconds(FireRate/60);
    allowFire = true;
    }
    
}