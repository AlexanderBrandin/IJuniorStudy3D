using UnityEngine;

public class CyclicForwardMover : MonoBehaviour
{
    private enum MovementDirection
    {
        Forward,
        Backward
    }

    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private bool _canMoveBackward;
    [SerializeField] private MovementDirection _startDirection;

    private MovementDirection _currentDirection;
    private float _coveredDistance;

    private void Awake()
    {
        _currentDirection = _startDirection;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float step = _speed * Time.deltaTime;
        Vector3 direction = GetDirection();

        transform.Translate(direction * step);
        _coveredDistance += step;

        if (_coveredDistance >= _distance)
        {
            ResetDistance();

            if (_canMoveBackward)
                ChangeDirection();
        }
    }

    private Vector3 GetDirection()
    {
        if (_currentDirection == MovementDirection.Forward)
            return Vector3.forward;

        return Vector3.back;
    }

    private void ResetDistance()
    {
        _coveredDistance = default;
    }

    private void ChangeDirection()
    {
        if (_currentDirection == MovementDirection.Forward)
            _currentDirection = MovementDirection.Backward;
        else
            _currentDirection = MovementDirection.Forward;
    }
}
