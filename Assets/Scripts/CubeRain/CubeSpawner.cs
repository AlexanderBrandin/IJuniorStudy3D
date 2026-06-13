using System.Collections;
using UnityEngine;

public class CubeSpawner : ObjectSpawner
{
    private const float HalfSizeDivider = 2f;

    [SerializeField] private FallingCube _cubePrefab;
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private Transform _spawnCenter;
    [SerializeField] private Transform _poolContainer;

    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Vector2 _spawnAreaSize;
    [SerializeField] private Color _startColor;
    [SerializeField] private float _minLifetime;
    [SerializeField] private float _maxLifetime;

    private PooledObjectPool<FallingCube> _pool;
    private Coroutine _spawningCoroutine;

    public override int TotalSpawned => _pool.TotalSpawned;
    public override int TotalCreated => _pool.TotalCreated;
    public override int ActiveCount => _pool.ActiveCount;

    private void Awake()
    {
        _pool = new PooledObjectPool<FallingCube>(
            CreateCube,
            ActivateCube,
            DeactivateCube,
            DestroyCube,
            _defaultCapacity,
            _maxSize
        );
    }

    private void OnEnable()
    {
        _spawningCoroutine = StartCoroutine(Spawning());
    }

    private void OnDisable()
    {
        if (_spawningCoroutine != null)
            StopCoroutine(_spawningCoroutine);
    }

    private IEnumerator Spawning()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            Spawn();

            yield return wait;
        }
    }

    private void Spawn()
    {
        FallingCube cube = _pool.Get(GetRandomSpawnPosition(), Random.rotation);

        cube.Initialize(_startColor, _minLifetime, _maxLifetime);

        NotifyStatisticsChanged();
    }

    private FallingCube CreateCube()
    {
        FallingCube cube = Instantiate(_cubePrefab, _poolContainer);

        cube.LifetimeExpired += HandleCubeLifetimeExpired;

        return cube;
    }

    private void ActivateCube(FallingCube cube)
    {
        cube.gameObject.SetActive(true);
    }

    private void DeactivateCube(FallingCube cube)
    {
        cube.gameObject.SetActive(false);
        cube.transform.SetParent(_poolContainer);
    }

    private void DestroyCube(FallingCube cube)
    {
        Destroy(cube.gameObject);
    }

    private void HandleCubeLifetimeExpired(FallingCube cube, Vector3 position)
    {
        _bombSpawner.Spawn(position);

        _pool.Release(cube);

        NotifyStatisticsChanged();
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float halfWidth = _spawnAreaSize.x / HalfSizeDivider;
        float halfDepth = _spawnAreaSize.y / HalfSizeDivider;

        float x = Random.Range(-halfWidth, halfWidth);
        float z = Random.Range(-halfDepth, halfDepth);

        return _spawnCenter.position + new Vector3(x, 0f, z);
    }
}
