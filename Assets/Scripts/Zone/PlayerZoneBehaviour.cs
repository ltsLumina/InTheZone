#region
using System;
using System.Collections;
using UnityEngine;
#endregion

public class PlayerZoneBehaviour : MonoBehaviour
{
    [SerializeField] bool inTheZone;
    [SerializeField] Canvas zoneCamOverlay;

    [SerializeField] int healthLossAmount = 2;
    [SerializeField] int healthGainAmount = 1;

    [SerializeField] float healthChangeRate = 1f;

    // Public referances
    Gun gun;
    Magazine mag;
    Health health;

    // Private referances
    GameObject currentZone;

    // onEnterZone event
    public delegate void OnEnterZone();
    public event OnEnterZone onEnterZone;

    public bool InTheZone
    {
        get => inTheZone;
        set => inTheZone = value;
    }

    void Awake()
    {
        // subscribe to the onEnterZone event
        onEnterZone += EnterZone;

        gun    = FindObjectOfType<Gun>();
        mag    = FindObjectOfType<Magazine>();
        health = FindObjectOfType<PlayerMovement>().GetComponent<Health>();
    }

    void EnterZone()
    {

    }

    void Start() => StartCoroutine(HealthLoop());

    void Update()
    {
        if (InTheZone && currentZone != null)
        {
            // Can shoot if in zone, but not reload.
            //zoneCamOverlay.enabled = true;
            gun.enabled   = true;
            mag.CanReload = false;
        }
        else
        {
            // Can not shoot if in zone, but can reload.
            //zoneCamOverlay.enabled = false;
            gun.enabled   = false;
            mag.CanReload = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        onEnterZone?.Invoke();
        currentZone = other.gameObject;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            InTheZone   = true;
            currentZone = other.gameObject; //TODO: MAY RETURN AN ERROR IF YOU COLLIDE WITH ENEMY
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            InTheZone   = false;
            currentZone = null;
        }
    }

    IEnumerator HealthLoop()
    {
        while (true)
        {
            if (InTheZone) health.CurrentHealth += healthGainAmount;
            else health.CurrentHealth           -= healthLossAmount;

            //// Clamp the health value between 0 and 100
            health.CurrentHealth = Mathf.Clamp(health.CurrentHealth, 0, 100);

            yield return new WaitForSeconds(healthChangeRate);
        }
    }
}