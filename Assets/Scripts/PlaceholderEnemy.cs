using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderEnemy : MonoBehaviour
{
    [SerializeField] float timeBeforeDestruction = 5f;

    private void Start()
    {
        StartCoroutine(EnemyDestructionRoutine());
    }

    // makes the enemy kill itself lol
    IEnumerator EnemyDestructionRoutine()
    {
        yield return new WaitForSeconds(timeBeforeDestruction);

        Destroy(gameObject);
    }
}
