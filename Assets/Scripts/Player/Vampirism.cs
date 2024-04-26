using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _vampiricDamagePerSecond = 15f;

    private Health _ownerHealth;
    private Health _targetHealth;

    private void Awake()
    {
        _ownerHealth = GetComponentInParent<Health>();
    }

    private void Update()
    {
        if (_targetHealth)
            DrainHealth();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            if (_targetHealth == null)
                _targetHealth = enemy.GetComponent<Health>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            _targetHealth = null;
    }

    private void DrainHealth()
    {
        _targetHealth.TakeDamage(_vampiricDamagePerSecond * Time.deltaTime);
        _ownerHealth.Heal(_vampiricDamagePerSecond * Time.deltaTime);
    }
}