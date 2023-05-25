using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneBehaviour : MonoBehaviour
{
    [SerializeField] bool inTheZone = false;
    PlayerController playerScript;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
        }
    }
}
