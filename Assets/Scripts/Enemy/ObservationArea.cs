using UnityEngine;

public class ObservationArea : MonoBehaviour
{
    [SerializeField] private bool _isPlayerDetected;
    private Transform _playerTransform;

    public bool IsPlayerDetected => _isPlayerDetected;
    public Transform PlayerTransform => _playerTransform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _playerTransform = player.transform;
            _isPlayerDetected = true;
        }
        else
        {
            _isPlayerDetected = false;  
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
            _isPlayerDetected = false;
    }
}