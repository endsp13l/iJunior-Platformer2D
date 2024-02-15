using UnityEngine;

public class Pursuit : MonoBehaviour
{
    [SerializeField] private ObservationArea _observationArea;
    [SerializeField] private float _speed = 2f;

    private void Update()
    {
        if (_observationArea.IsPlayerDetected)
        {
            transform.position = Vector3.MoveTowards(transform.position, _observationArea.PlayerTransform.position,
                _speed * Time.deltaTime);
        }
    }
}