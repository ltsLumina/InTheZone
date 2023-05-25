using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoneBehaviour : MonoBehaviour
{
    [SerializeField] bool inTheZone = false;
    [SerializeField] Canvas zoneCamOverlay;

    private GameObject currentZone;

   public Gun gunScript;

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

    
}
