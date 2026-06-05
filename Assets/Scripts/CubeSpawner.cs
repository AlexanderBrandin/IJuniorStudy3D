using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private FallingCube _cubePrefab;
    [SerializeField] private CubeColorizer _colorizer;
    [SerializeField] private Transform _spawnCenter;
    [SerializeField] private Transform _poolContainer;

    [SerializeField] private int _initialPoolSize;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Vector2 _spawnAreaSize;
    [SerializeField] private Color _startColor;

    private ObjectPool<FallingCube> _pool;
    private Coroutine _spawnCoroutine;

    private void Awake()
    {
        _pool = new ObjectPool<FallingCube>(_cubePrefab, _initialPoolSize, _poolContainer);
    }

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnCubes());
    }

    private void OnDisable()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
    }

    private IEnumerator SpawnCubes()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            SpawnCube();

            yield return wait;
        }
    }

    private void SpawnCube()
    {
        FallingCube cube = _pool.Get();

        cube.LifeTimeExpired += ReleaseCube;
        cube.Initialize(_colorizer, _startColor);

        cube.transform.position = GetRandomSpawnPosition();
        cube.transform.rotation = Random.rotation;
    }

    private void ReleaseCube(FallingCube cube)
    {
        cube.LifeTimeExpired -= ReleaseCube;

        _pool.Release(cube);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float halfWidth = _spawnAreaSize.x / 2f;
        float halfDepth = _spawnAreaSize.y / 2f;

        float x = Random.Range(-halfWidth, halfWidth);
        float z = Random.Range(-halfDepth, halfDepth);

        return _spawnCenter.position + new Vector3(x, 0f, z);
    }
}
