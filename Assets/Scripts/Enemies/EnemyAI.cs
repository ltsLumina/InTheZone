using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    float distanceToTarget = Mathf.Infinity;

    protected PlayerController player;
    
    protected Transform target;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        player = FindObjectOfType<PlayerController>();
        target = player.transform;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
    }

    protected virtual IEnumerator Knockback()
    {
        
        
        return null;
    }
}
