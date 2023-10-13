using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _isOnGround = false;
    private int _coins = 0;
    
    public int Coins => _coins;
    public bool IsOnGround => _isOnGround;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && transform.position.y > collision.transform.position.y)
            _isOnGround = true;
        else 
            _isOnGround = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            _isOnGround = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Coin>(out Coin coin))
            _coins += coin.Collect();
    }
}