using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float _maxHealth = 100f;
    private float _currentHealth;

    public event Action HealthChanged;

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
        _currentHealth -= damage;

        if (_currentHealth < 0)
            _currentHealth = 0;

        HealthChanged?.Invoke();
    }

    public void Heal(float value)
    {
        _currentHealth += value;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        HealthChanged?.Invoke();
    }
}