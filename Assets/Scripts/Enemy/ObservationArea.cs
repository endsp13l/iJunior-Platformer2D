using UnityEngine;
using UnityEngine.Events;

public class ObservationArea : MonoBehaviour
{
    private Transform _playerTransform;

    private UnityEvent _playerDetected = new UnityEvent();
    private UnityEvent _playerLost = new UnityEvent();

    public Transform PlayerTransform => _playerTransform;

    public event UnityAction PlayerDetected
    {
        add => _playerDetected.AddListener(value);
        remove => _playerDetected.RemoveListener(value);
    }

    public event UnityAction PlayerLost
    {
        add => _playerLost.AddListener(value);
        remove => _playerLost.RemoveListener(value);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _playerTransform = player.transform;
            _playerDetected?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
            _playerLost?.Invoke();
    }
}