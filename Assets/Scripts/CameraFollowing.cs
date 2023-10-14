using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed = 2f;

    [SerializeField] private float _leftLimit;
    [SerializeField] private float _rightLimit;
    [SerializeField] private float _topLimit;
    [SerializeField] private float _bottomLimit;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 nextPosition =
            Vector3.Lerp(transform.position, _target.position + _offset, _speed * Time.fixedDeltaTime);
        transform.position = nextPosition;

        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(Mathf.Clamp(currentPosition.x, _leftLimit, _rightLimit),
            Mathf.Clamp(currentPosition.y, _bottomLimit, _topLimit), currentPosition.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(new Vector3(_leftLimit, _bottomLimit, 0), new Vector3(_leftLimit, _topLimit, 0));
        Gizmos.DrawLine(new Vector3(_rightLimit, _bottomLimit, 0), new Vector3(_rightLimit, _topLimit, 0));

        Gizmos.DrawLine(new Vector3(_leftLimit, _topLimit, 0), new Vector3(_rightLimit, _topLimit, 0));
        Gizmos.DrawLine(new Vector3(_rightLimit, _bottomLimit, 0), new Vector3(_leftLimit, _bottomLimit, 0));
    }
}