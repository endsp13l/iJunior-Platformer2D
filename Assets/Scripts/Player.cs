using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _isOnGround = false;

    public bool IsOnGround => _isOnGround;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && transform.position.y > collision.transform.position.y)
            _isOnGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            _isOnGround = false;
    }
}