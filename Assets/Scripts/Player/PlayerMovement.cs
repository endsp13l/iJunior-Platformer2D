using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 100;

    private Rigidbody2D _playerRigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Player _player;

    private bool isInAir = false;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.anyKey)
            Move();

        if (Input.GetKey(KeyCode.Space) && isInAir == false)
            Jump();

        if (_player.IsOnGround)
            isInAir = false;
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _spriteRenderer.flipX = true;
            transform.Translate(Vector2.left * (_speed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _spriteRenderer.flipX = false;
            transform.Translate(Vector2.right * (_speed * Time.deltaTime));
        }
    }

    private void Jump()
    {
        isInAir = true;
        _playerRigidbody.AddForce(Vector2.up * (_jumpForce * Time.deltaTime), ForceMode2D.Impulse);
    }
}