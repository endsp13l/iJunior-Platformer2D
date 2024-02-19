using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 100;

    private Rigidbody2D _playerRigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private Vector2 _direction;
    private float _horizontalInput;
    
    private bool _isInAir = false;
    private bool _isOnGround = false;
    
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsOnGround = Animator.StringToHash("IsOnGround");
    private static readonly int Jump1 = Animator.StringToHash("Jump");
    
    public Vector3 Direction => _direction;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _horizontalInput = Input.GetAxis(Horizontal);

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

        if (_isOnGround)
        {
            _animator.SetBool(IsOnGround, true);
            _isInAir = false;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>() && transform.position.y > collision.transform.position.y)
            _isOnGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
            _isOnGround = false;
    }

    private void Move()
    {
        if (_horizontalInput < 0)
        {
            _spriteRenderer.flipX = true;
            _direction = Vector2.left;
        }
        else if (_horizontalInput >= 0)
        {
            _spriteRenderer.flipX = false;
            _direction = Vector2.right;
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