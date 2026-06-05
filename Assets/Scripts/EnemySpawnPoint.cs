using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _target;

    public Enemy Spawn()
    {
        Enemy enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);

        enemy.Initialize(_target);

        return enemy;
    }
}
