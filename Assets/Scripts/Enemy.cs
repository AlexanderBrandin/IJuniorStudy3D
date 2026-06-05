using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _target;

    private void Update()
    {
        MoveToTarget();
    }

    public void Initialize(Transform target)
    {
        _target = target;
    }

    private void MoveToTarget()
    {
        if (_target == null)
            return;

        Vector3 direction = _target.position - transform.position;
        Vector3 normalizedDirection = direction.normalized;

        transform.Translate(normalizedDirection * _speed * Time.deltaTime, Space.World);
    }
}
