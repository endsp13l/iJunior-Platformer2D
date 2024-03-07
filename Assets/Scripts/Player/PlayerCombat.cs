using UnityEngine;
using TMPro;
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

    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private SmoothHealthBar _smoothHealthBar;

    private PlayerMovement _playerMovement;

    private bool _isAlive => _currentHealth > 0;
    private float _destroyDelay = 0.1f;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _smoothHealthBar.SetMaxHealth(_maxHealth);
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        _healthText.text = $"Health: {_currentHealth} / {_maxHealth}";
        _healthBar.value = _currentHealth / _maxHealth;

        if (_isAlive && Input.GetMouseButtonDown(0))
            Attack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out AidKit aidKit))
            Heal(aidKit.Collect());
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
            Destroy(gameObject, _destroyDelay);
        }

        _smoothHealthBar.SetHealth(_currentHealth);
    }

    public void Heal(float value)
    {
        _currentHealth += value;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        _smoothHealthBar.SetHealth(_currentHealth);
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