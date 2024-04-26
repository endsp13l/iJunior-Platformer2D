using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerCombat : MonoBehaviour
{
    [Header("AttackStats")] 
    [SerializeField] private float _attackDamage = 20f;
    [SerializeField] private float _attackDistance = 2f;

    [Header("VampirismStats")] 
    [SerializeField] private float _vampirismDuration = 3f;
    [SerializeField] private Vampirism _vampirismArea;

    [Header("Layers")] 
    [SerializeField] private LayerMask _enemyLayer;

    private PlayerMovement _playerMovement;
    private Enemy _enemy;
    private bool _isVampirismActive;
    private Coroutine _coroutine;

    public event Action<Vector3> Hit;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Attack();

        if (Input.GetMouseButtonDown(1))
            UseVampirism();
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
                targetHealth.TakeDamage(_attackDamage);
                Hit?.Invoke(hit.transform.position);
            }
        }
    }

    private void UseVampirism()
    {
        if (_isVampirismActive)
            return;

        _isVampirismActive = true;
        _vampirismArea.gameObject.SetActive(true);
        _coroutine = StartCoroutine(ActivateVampirism());
    }

    private IEnumerator ActivateVampirism()
    {
        WaitForSeconds wait = new WaitForSeconds(_vampirismDuration);

        yield return wait;

        DeactivateVampirism();
    }

    private void DeactivateVampirism()
    {
        if (_coroutine != null)
        {
            _isVampirismActive = false;
            StopCoroutine(_coroutine);
            _vampirismArea.gameObject.SetActive(false);
        }
    }
}