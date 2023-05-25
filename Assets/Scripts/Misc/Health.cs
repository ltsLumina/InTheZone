#region
using System;
using UnityEngine;
using static Interfaces;
#endregion

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            if (CurrentHealth <= 0) Death();
        }
    }

    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage!");
    }

    public void Death()
    {
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject);
    }
}