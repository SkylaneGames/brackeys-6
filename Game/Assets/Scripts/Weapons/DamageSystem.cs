using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public event Action Destroyed;

    [SerializeField]
    private float _maxHealth = 100f;
    public float MaxHealth => _maxHealth;

    private float _currentHealth;
    public float CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
        }
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage(float amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth == 0)
        {
            Destroyed?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth += amount;
    }
}
