using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Health))]
public class Collector : MonoBehaviour
{
    public event Action<int> CoinCollected;

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
            CoinCollected?.Invoke(coin.Collect());

        if (other.TryGetComponent(out AidKit aidKit))
            _health.Heal(aidKit.Collect());
    }
}