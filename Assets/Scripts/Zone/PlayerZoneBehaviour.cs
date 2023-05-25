using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoneBehaviour : MonoBehaviour
{
   [SerializeField] bool inTheZone = false;
   [SerializeField] Canvas zoneCamOverlay;

   [SerializeField] int healthLossAmount = 2;
   [SerializeField] int healthGainAmount = 1;

    [SerializeField] float healthChangeRate = 1f;

    // Public referances
    public Gun gunScript;
    public Health healthScript;
    // Private referances
    private GameObject currentZone;


    private void Awake()
    {
        StartCoroutine(HealthLoop());
    }

    void Update()
    {
        if (inTheZone && currentZone != null)
        {
            zoneCamOverlay.enabled = true;
            gunScript.CanFire = true;
        }
        else
        {
            zoneCamOverlay.enabled = false;
            gunScript.CanFire = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Zone")
        {
            inTheZone = true;
            currentZone = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Zone")
        {
            inTheZone = false;
            currentZone = null;
        }
    }

    private IEnumerator HealthLoop()
    {
        while (true)
        {
            if (inTheZone)
            {
                healthScript.CurrentHealth  += healthGainAmount;
            }
            else
            {
                healthScript.CurrentHealth -= healthLossAmount;
            }

            //// Clamp the health value between 0 and 100
            //healthScript.currentHealth = Mathf.Clamp(healthScript.currentHealth, 0f, 100f);

            yield return new WaitForSeconds(healthChangeRate);
        }
    }
}
