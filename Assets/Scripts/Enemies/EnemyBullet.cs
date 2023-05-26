using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] int bulletDamage = 2;
    Health playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<Health>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (CompareTag("Player"))
        {
            playerHealth.CurrentHealth -= bulletDamage;
        } 
        Destroy(gameObject);
    }
}
