using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnPoint[] _spawnPoints;
    [SerializeField] private float _spawnDelay;

    private Coroutine _spawnCoroutine;

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    private void OnDisable()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            EnemySpawnPoint spawnPoint = GetRandomSpawnPoint();

            spawnPoint.Spawn();

            yield return wait;
        }
    }

    private EnemySpawnPoint GetRandomSpawnPoint()
    {
        int index = Random.Range(0, _spawnPoints.Length);

        return _spawnPoints[index];
    }
}
