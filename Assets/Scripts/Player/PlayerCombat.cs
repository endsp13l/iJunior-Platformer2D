using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float _damage = 20f;
    [SerializeField] private float _attackDistance = 2f;
    [SerializeField] private LayerMask _enemyLayer;

    private PlayerMovement _playerMovement;
    private Health _health;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        if (_health.IsAlive && Input.GetMouseButtonDown(0))
            Attack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out AidKit aidKit))
            _health.Heal(aidKit.Collect());
    }

   private void Attack()
    {
        Vector2 direction = _playerMovement.Direction;
        Ray2D forwardRay = new Ray2D(transform.position, direction);

        RaycastHit2D hit = Physics2D.Raycast(forwardRay.origin, direction, _attackDistance, _enemyLayer);

        if (hit.collider.TryGetComponent(out Health targetHealth))
            targetHealth.TakeDamage(_damage);
    }
}