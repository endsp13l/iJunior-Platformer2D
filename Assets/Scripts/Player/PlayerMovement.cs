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

    private float _horizontalInput;
    private bool _isInAir = false;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsOnGround = Animator.StringToHash("IsOnGround");

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        if (_horizontalInput != 0)
        {
            _animator.SetBool(IsRunning, true);
            Move();
        }
        else
        {
            _animator.SetBool(IsRunning, false);
        }

        if (Input.GetKey(KeyCode.Space) && _isInAir == false)
        {
            _animator.SetTrigger("Jump");
            Jump();
        }

        if (_player.IsOnGround)
        {
            _animator.SetBool(IsOnGround, true);
            _isInAir = false;
        }
    }

    private void Move()
    {
        if (_horizontalInput < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_horizontalInput >= 0)
        {
            _spriteRenderer.flipX = false;
        }

        transform.Translate(Vector2.right * (_horizontalInput * _speed * Time.deltaTime));
    }

    private void Jump()
    {
        _isInAir = true;
        _animator.SetBool(IsOnGround, false);
        _playerRigidbody.AddForce(Vector2.up * (_jumpForce * Time.deltaTime), ForceMode2D.Impulse);
    }
}