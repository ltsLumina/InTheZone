using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float attackTimer = 4f;
    [SerializeField] float knockbackAmount;
    [SerializeField] float knockbackMultiplier;
    [SerializeField] int meleeDamage = 2;
    [SerializeField] AudioSource walkSFX;
    
    Rigidbody playerRigidbody;
    protected NavMeshAgent navMeshAgent;
    protected Animator anim;
    
    protected float distanceToTarget = Mathf.Infinity;
    protected PlayerMovement player;
    protected Health playerHealth;

    protected Transform target;
    
    protected bool attacking = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        player          = FindObjectOfType<PlayerMovement>();
        playerHealth    = FindObjectOfType<Health>();
        target          = player.transform;

        navMeshAgent  = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        
        EngageTarget();
        AnimationHandler();
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

        playerHealth.CurrentHealth -= meleeDamage;
        
        Vector3 knockbackVector = Vector3.Normalize(target.position - transform.position) * knockbackAmount;
        
        target.GetComponent<Rigidbody>().velocity = knockbackVector * knockbackMultiplier;
        
        Debug.Log("ahhhhh");
        yield return new WaitForSeconds(attackTimer);
        attacking = false;
    }

    void AnimationHandler()
    {
        // check if navagent is moving and set "Move" animator bool accordingly
        anim.SetBool("Walk", navMeshAgent.velocity.magnitude > 0);

        // if idle and not attacking, set "Idle" animator bool accordingly
        if (navMeshAgent.velocity.magnitude == 0 && !attacking)
        {
            anim.SetBool("Idle", true);
        }
        else
        {
            anim.SetBool("Idle", false);
            walkSFX.Play();
            // walking
        }

        // if attacking, set "Attack" animator bool accordingly
        if (gameObject.name == "MeleeEnemy")
        {
            anim.SetBool("MeleeAttack", attacking);
        }
        else if (gameObject.name == "RangedEnemy")
        {
            anim.SetBool("RangedAttack", attacking);
        }
    }
}