using UnityEngine;

public class Player : MonoBehaviour
{
    private int _coins = 0;

    public int Coins => _coins;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
            _coins += coin.Collect();
    }
}