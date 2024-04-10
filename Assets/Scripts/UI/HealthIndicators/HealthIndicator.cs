using UnityEngine;

public abstract class HealthIndicator : MonoBehaviour
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

    protected abstract void SetHealth();
}