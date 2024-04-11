using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float _damage = 15f;
    [SerializeField] private float _nextAttakTime = 2f;

    private bool _canAttack = false;
    private Health _playerHealth;
    private Coroutine _coroutine;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;

        if (target.TryGetComponent(out Player player) && target.TryGetComponent(out Health health))
        {
            _playerHealth = health;
            _canAttack = true;
            _coroutine = StartCoroutine(Attack());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            _canAttack = false;

            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }
    }

    private IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(_nextAttakTime);

        while (_canAttack)
        {
            _playerHealth.TakeDamage(_damage);
            yield return wait;
        }
    }
}