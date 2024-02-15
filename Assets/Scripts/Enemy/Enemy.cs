using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Pursuit _pursuit;
    [SerializeField] private Patroling _patroling;
    [SerializeField] private ObservationArea _observationArea;

    private void Update()
    {
        if (_observationArea.IsPlayerDetected)
        {
            _pursuit.enabled = true;
            _patroling.enabled = false;
        }
        else
        {
            _pursuit.enabled = false;
            _patroling.enabled = true;
        }
    }
}