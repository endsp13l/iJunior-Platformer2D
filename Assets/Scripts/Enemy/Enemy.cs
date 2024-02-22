using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _damage = 15f;
    [SerializeField] private float _nextAttakTime = 2f;

    private PlayerCombat _player;
    private bool _canAttack = false;

   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerCombat player))
        {
            _player = player;
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
    
    public void TakeDamage(float damage)
    {
        _health -= damage;
        
        if (_health <= 0)
            Destroy(gameObject);
    }
    
    private IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(_nextAttakTime);

        while (_canAttack)
        {
            _player.TakeDamage(_damage);
            yield return wait;
        }
    }
}