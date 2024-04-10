using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    private Collector _collector;
    private int _coins = 0;

    public int Coins => _coins;

    private void Awake()
    {
        _collector = GetComponent<Collector>();
    }

    private void OnEnable()
    {
        _collector.CoinCollected += AddCoins;
    }

    private void AddCoins(int value)
    {
        if (value > 0)
            _coins += value;
    }

    private void OnDisable()
    {
        _collector.CoinCollected -= AddCoins;
    }
}