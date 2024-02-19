using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _damage = 15f;

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    private void FixedUpdate()
    {
        if (_health <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerCombat player))
            player.TakeDamage(_damage);
    }
}