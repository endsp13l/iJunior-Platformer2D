using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const float _minHealth = 0f;
    private float _maxHealth = 100f;
    private float _currentHealth;

    public event Action HealthChanged;
    public event Action Died;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        HealthChanged?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            _currentHealth -= damage;
            _currentHealth = Math.Clamp(_currentHealth - damage, _minHealth, _maxHealth); 

            HealthChanged?.Invoke();

            if (_currentHealth <= _minHealth)
            {
                Died?.Invoke();
            }
        }
    }

    public void Heal(float value)
    {
        if (value > 0)
        {
            _currentHealth = Math.Clamp(_currentHealth + value, _minHealth, _maxHealth); 
            
            HealthChanged?.Invoke();
        }
    }
}