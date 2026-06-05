using UnityEngine;

public class PatrolTarget : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;

    private int _currentPointIndex;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_points.Length == 0)
            return;

        Transform currentPoint = _points[_currentPointIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            currentPoint.position,
            _speed * Time.deltaTime
        );

        if (transform.position == currentPoint.position)
            SwitchToNextPoint();
    }

    private void SwitchToNextPoint()
    {
        _currentPointIndex++;

        if (_currentPointIndex >= _points.Length)
            _currentPointIndex = 0;
    }
}
