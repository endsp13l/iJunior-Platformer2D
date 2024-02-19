using UnityEngine;

[RequireComponent(typeof(Pursuit))]
[RequireComponent(typeof(Patroling))]
[RequireComponent(typeof(ObservationArea))]

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Pursuit _pursuit;
    [SerializeField] private Patroling _patroling;
    [SerializeField] private ObservationArea _observationArea;

    private void OnEnable()
    {
        _observationArea.PlayerDetected += Pursue;
        _observationArea.PlayerLost += Patrol;
    }

    private void OnDisable()
    {
        _observationArea.PlayerDetected -= Pursue;
        _observationArea.PlayerLost -= Patrol;
    }

    private void Pursue()
    {
        _patroling.enabled = false;
        _pursuit.enabled = true;
        _pursuit.SetTarget(_observationArea.PlayerTransform);
    }

    private void Patrol()
    {
        _pursuit.enabled = false;
        _patroling.enabled = true;
    }
}
