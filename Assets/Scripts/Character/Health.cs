using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float _maxHealth = 100f;
    private float _currentHealth;

    public event Action HealthChanged;
    public event Action Died;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;
    public bool IsAlive => _currentHealth > 0;

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

            if (_currentHealth < 0)
            {
                _currentHealth = 0;
                Died?.Invoke();
            }

            HealthChanged?.Invoke();
        }
    }

    public void Heal(float value)
    {
        if (value > 0)
        {
            _currentHealth += value;

            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }

            HealthChanged?.Invoke();
        }
    }
}