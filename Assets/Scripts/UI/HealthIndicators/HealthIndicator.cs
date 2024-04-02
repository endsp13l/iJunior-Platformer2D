using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField] protected Health _health;

    private void OnEnable()
    {
        _health.HealthChanged += SetHealth;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= SetHealth;
    }

    protected virtual void SetHealth()
    {
    }
}