#region
using System;
using UnityEditor;
using UnityEngine;
using static Interfaces;
#endregion

public class Health : MonoBehaviour, IDamageable
{
    [Header("Health Options")]
    [SerializeField] int maxHealth;
    [SerializeField, Range(0,100)] int currentHealth;

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }
    private void Update()
    {
        Debug.Log(CurrentHealth);
    }
    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
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