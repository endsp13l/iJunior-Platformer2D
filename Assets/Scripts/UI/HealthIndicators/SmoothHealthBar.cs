using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothHealthBar : HealthIndicator
{
    [SerializeField] private float _valueChangeSpeed = 0.3f;

    private Slider _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
    }

    protected override void SetHealth()
    {
        float value = _health.CurrentHealth / _health.MaxHealth;

        StartCoroutine(SmoothChangeValue(value));
        StopCoroutine(SmoothChangeValue(value));
    }

    private IEnumerator SmoothChangeValue(float value)
    {
        float startValue = _healthBar.value;
        float targetValue = value;

        while (_healthBar.value != targetValue)
        {
            _healthBar.value = Mathf.MoveTowards(startValue, targetValue, _valueChangeSpeed * Time.deltaTime);
            startValue = _healthBar.value;

            yield return null;
        }
    }
}