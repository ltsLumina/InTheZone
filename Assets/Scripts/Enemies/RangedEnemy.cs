using System.Collections;
using UnityEngine;

public class RangedEnemy : EnemyAI
{
    [SerializeField] float attackRange = 2f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] float projectileForce = 2f;
    [SerializeField] float attackCooldown = 2f;

    protected override void EngageTarget()
    {
        base.EngageTarget();

        float rangeCheck = navMeshAgent.stoppingDistance * attackRange;

        if (distanceToTarget <= rangeCheck && distanceToTarget >= navMeshAgent.stoppingDistance + 0.5f)
        {
            AttackTargetAtRange();
            Debug.Log("works? 2");
        }
        
    }

    protected override void ChaseTarget()
    {
        float rangeCheck = navMeshAgent.stoppingDistance * attackRange;

        if (distanceToTarget <= rangeCheck)
        {
            navMeshAgent.isStopped = true;
            return;
        }
        else
        {
            navMeshAgent.isStopped = false;
        }
        
        base.ChaseTarget();
    }

    void AttackTargetAtRange()
    {
        if (attacking) return;

        StartCoroutine(PerformAttack());
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator PerformAttack()
    {
        attacking = true;
        Debug.Log(name + " is attacking " + target.name);

        // Calculate the direction to the target
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;
        direction.Normalize();

        // Rotate the enemy towards the target
        transform.rotation = Quaternion.LookRotation(-direction);

        // Instantiate the projectile and set its initial position and rotation
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        // Apply a force to the projectile in the calculated direction
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.velocity = direction * projectileForce;

        // Wait for the attack cooldown
        yield return new WaitForSeconds(attackCooldown);

        attacking = false;
    }
}
