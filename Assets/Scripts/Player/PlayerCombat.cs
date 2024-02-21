using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Text _healthText;

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
        _healthText.text = "Health: " + _currentHealth;
        
        if (_isAlive && Input.GetMouseButtonDown(0))
            Attack();
    }

    private void FixedUpdate()
    {
        if (_isAlive)
            return;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out AidKit aidKit))
        {
            _currentHealth += aidKit.Collect();
            if (_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;
        }
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
}