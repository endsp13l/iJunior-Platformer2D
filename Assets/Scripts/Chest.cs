using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Coin _prize;
    [SerializeField] private float _prizeSpawnOffset = 1f;

    private bool _isOpened = false;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player) && _isOpened == false)
            Open();
    }

    private void Open()
    {
        Vector2 prizePosition = new Vector2(transform.position.x, transform.position.y + _prizeSpawnOffset);

        _animator.SetBool(IsOpen, true);
        Instantiate(_prize, prizePosition, Quaternion.identity);
        _isOpened = true;
    }
}