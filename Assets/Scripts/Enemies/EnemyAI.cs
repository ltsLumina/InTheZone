using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float attackTimer = 4f;
    [SerializeField] float knockbackAmount;
    [SerializeField] float knockbackMultiplier;
    
    Rigidbody playerRigidbody;
    protected NavMeshAgent navMeshAgent;
    
    protected float distanceToTarget = Mathf.Infinity;
    protected PlayerMovement player;
    protected Health playerHealth;

    protected Transform target;
    
    protected bool attacking = false;

    public virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        playerRigidbody  = GetComponent<Rigidbody>();
        player           = FindObjectOfType<PlayerMovement>();
        target           = player.transform;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        
        EngageTarget();
    }
    
    protected virtual void EngageTarget()
    {
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }

    }
    
    protected virtual void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }
    
    protected virtual void AttackTarget()
    {
        if (attacking) return;
        
        StartCoroutine(Knockback());
        Debug.Log("knockback is called");
    }

        //HEJ JAG HETER DENNIS OCH TYCKER ATT NI GÖR ETT MYSIGT SPEL :D ^___^ MEN SLUTA SKJUTA PINGVINER FÖR I HELVETE!!!
    
    IEnumerator Knockback()
    {
        attacking = true;
        Debug.Log(name + " is attacking " + target.name);

        //playerHealth.CurrentHealth--;
        
        Vector3 knockbackVector = Vector3.Normalize(target.position - transform.position) * knockbackAmount;
        
        target.GetComponent<Rigidbody>().velocity = knockbackVector * knockbackMultiplier;
        
        Debug.Log("ahhhhh");
        yield return new WaitForSeconds(attackTimer);
        attacking = false;
    }
}