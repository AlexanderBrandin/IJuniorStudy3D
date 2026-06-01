using UnityEngine;

public class CyclicUniformScaler : MonoBehaviour
{
    private enum ScaleDirection
    {
        Increase,
        Decrease
    }

    [SerializeField] private float scaleSpeed;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private ScaleDirection startDirection;

    private ScaleDirection _currentDirection;

    private void Awake()
    {
        _currentDirection = startDirection;
    }

    private void Update()
    {
        Scale();
    }

    private void Scale()
    {
        Vector3 currentScale = transform.localScale;
        Vector3 scaleStep = Vector3.one * scaleSpeed * Time.deltaTime;
        Vector3 newScale = GetNextScale(currentScale, scaleStep);

        transform.localScale = ClampScale(newScale);

        if (HasReachedLimit())
        {
            ChangeDirection();
        }
    }

    private Vector3 GetNextScale(Vector3 currentScale, Vector3 scaleStep)
    {
        if (_currentDirection == ScaleDirection.Increase)
            return currentScale + scaleStep;

        return currentScale - scaleStep;
    }

    private Vector3 ClampScale(Vector3 scale)
    {
        float clampedScale = Mathf.Clamp(scale.x, minScale, maxScale);

        return Vector3.one * clampedScale;
    }

    private bool HasReachedLimit()
    {
        if (_currentDirection == ScaleDirection.Increase)
            return transform.localScale.x >= maxScale;

        return transform.localScale.x <= minScale;
    }

    private void ChangeDirection()
    {
        if (_currentDirection == ScaleDirection.Increase)
            _currentDirection = ScaleDirection.Decrease;
        else
            _currentDirection = ScaleDirection.Increase;
    }
}
