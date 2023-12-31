using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

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
    private static readonly int Jump1 = Animator.StringToHash("Jump");

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
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
            _animator.SetTrigger(Jump1);
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

        _playerRigidbody.velocity = new Vector2(_horizontalInput * _speed, _playerRigidbody.velocity.y);
    }

    private void Jump()
    {
        _isInAir = true;
        _animator.SetBool(IsOnGround, false);
        _playerRigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}