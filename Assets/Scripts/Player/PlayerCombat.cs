using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCombat : MonoBehaviour
{
    [Header("Health")] 
    [SerializeField] private float _currentHealth = 100f;
    [SerializeField] private float _maxHealth = 100f;
    [Header("Attack")] 
    [SerializeField] private float _damage = 20f;
    [SerializeField] private float _attackDistance = 2f;

    [SerializeField] private LayerMask _enemyLayer;

    private PlayerMovement _playerMovement;

    private bool _isAlive => _currentHealth > 0;

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (_isAlive && Input.GetMouseButtonDown(0))
            Attack();
    }

    private void FixedUpdate()
    {
        if (_isAlive)
            return;

        Destroy(gameObject);
    }

    private void Attack()
    {
        Vector2 direction = _playerMovement.Direction;
        Ray2D forwardRay = new Ray2D(transform.position, direction);

        RaycastHit2D hit = Physics2D.Raycast(forwardRay.origin, direction, _attackDistance, _enemyLayer);

        if (hit.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 startPosition = transform.position;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPosition, _attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(startPosition, _playerMovement.Direction);
    }
}