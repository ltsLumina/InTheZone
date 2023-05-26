using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderEnemy : MonoBehaviour
{
    [SerializeField] float timeBeforeDestruction = 5f;
    [SerializeField] ParticleSystem enemyHitParticle;
    [SerializeField] ParticleSystem enemyDeathParticle;

    private void Start()
    {
        StartCoroutine(EnemyDestructionRoutine());
    }

    // makes the enemy kill itself lol
    IEnumerator EnemyDestructionRoutine()
    {
        enemyDeathParticle.Play();

        yield return new WaitForSeconds(timeBeforeDestruction);

        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            enemyHitParticle.Play();
        }
    }
}