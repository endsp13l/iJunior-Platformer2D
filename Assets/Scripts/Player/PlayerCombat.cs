using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float _damage = 20f;
    [SerializeField] private float _attackDistance = 2f;
    [SerializeField] private LayerMask _enemyLayer;

    private PlayerMovement _playerMovement;

    public event Action<Vector3> Hit;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Attack();
    }

    private void Attack()
    {
        Vector2 direction = _playerMovement.Direction;
        Ray2D forwardRay = new Ray2D(transform.position, direction);

        RaycastHit2D hit = Physics2D.Raycast(forwardRay.origin, direction, _attackDistance, _enemyLayer);
        Collider2D target = hit.collider;

        if (target)
        {
            if (target.TryGetComponent(out Health targetHealth))
            {
                targetHealth.TakeDamage(_damage);
                Hit?.Invoke(hit.transform.position);
            }
        }
    }
}