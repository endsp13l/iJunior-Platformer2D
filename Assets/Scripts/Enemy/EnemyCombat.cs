using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float _damage = 15f;
    [SerializeField] private float _nextAttakTime = 2f;

    private bool _canAttack = false;
    private Health _playerHealth;

   private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;
        
        if (target.GetComponent<Player>() && target.TryGetComponent<Health>(out Health health))
        {
            _playerHealth = health;
            _canAttack = true;
            StartCoroutine(Attack());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerCombat>())
        {
           _canAttack = false;
            StopCoroutine(Attack());
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