using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothHealthBar : HealthIndicator
{
    [SerializeField] private float _valueChangeSpeed = 0.3f;

    private Slider _healthBar;
    private Coroutine _coroutine;
  
    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
    }

    protected override void SetHealth()
    {
        float value = _health.CurrentHealth / _health.MaxHealth;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
        
        _coroutine = StartCoroutine(SmoothChangeValue(value));
       
        if (_coroutine != null && _healthBar.value == value) 
            StopCoroutine(_coroutine);
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