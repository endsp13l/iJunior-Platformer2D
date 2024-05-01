using System.Collections;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    private const string EnemyLayer = nameof(Enemy);

    [SerializeField] private float _vampiricDamage = 15f;
    [SerializeField] private float _drainHealthPeriod = 1f;
    [SerializeField] private float _vampirismRadius = 2f;

    private Health _ownerHealth;
    private Collider2D _target;
    private Coroutine _coroutine;

    private void Awake()
    {
        _ownerHealth = GetComponentInParent<Health>();
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(SetTarget());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator SetTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(_drainHealthPeriod);

        yield return wait;
        _target = Physics2D.OverlapCircle(transform.position, _vampirismRadius, LayerMask.GetMask(EnemyLayer));

        if (_target)
            DrainHealth(_target);
    }

    private void DrainHealth(Collider2D target)
    {
        target.TryGetComponent(out Health targetHealth);

        if (targetHealth)
        {
            targetHealth.TakeDamage(_vampiricDamage);
            _ownerHealth.Heal(_vampiricDamage);
        }
    }
}