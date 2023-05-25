using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : EnemyAI
{
    [SerializeField] float attackRange = 2f;

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
            navMeshAgent.Stop();
            return;
        }
        base.ChaseTarget();
    }

    void AttackTargetAtRange()
    {
        
    }
}
