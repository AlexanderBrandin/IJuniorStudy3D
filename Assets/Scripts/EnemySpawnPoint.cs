using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform _moveDirectionPoint;

    public Vector3 Position => transform.position;

    public Vector3 Direction => _moveDirectionPoint.position - transform.position;
}
