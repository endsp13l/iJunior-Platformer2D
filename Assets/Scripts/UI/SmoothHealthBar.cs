using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBar : MonoBehaviour
{
    [SerializeField] private float _healthChangeSpeed = 20f;

    private Slider _healthBar;
    private float _maxHealth;
    private float _currentHealth;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
    }

    public void SetMaxHealth(float health)
    {
        _maxHealth = health;
        _currentHealth = _maxHealth;

        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _maxHealth;
    }

    public void SetHealth(float health)
    {
        StartCoroutine(ChangeHealth(health));
        StopCoroutine(ChangeHealth(health));
    }

    private IEnumerator ChangeHealth(float health)
    {
        while (_currentHealth != health)
        {
            _healthBar.value = Mathf.MoveTowards(_currentHealth, health, _healthChangeSpeed * Time.deltaTime);
            _currentHealth = _healthBar.value;

            yield return null;
        }
    }
}