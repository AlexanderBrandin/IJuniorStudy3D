using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<FallingCube>
{
    private const float HalfSizeDivider = 2f;

    [SerializeField] private FallingCube _cubePrefab;
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private Transform _spawnCenter;

    [SerializeField] private float _spawnDelay;
    [SerializeField] private Vector2 _spawnAreaSize;
    [SerializeField] private Color _startColor;
    [SerializeField] private float _minLifetime;
    [SerializeField] private float _maxLifetime;

    private Coroutine _spawningCoroutine;

    private void OnEnable()
    {
        _spawningCoroutine = StartCoroutine(Spawning());
    }

    private void OnDisable()
    {
        if (_spawningCoroutine != null)
            StopCoroutine(_spawningCoroutine);
    }

    protected override FallingCube CreateObject()
    {
        FallingCube cube = Instantiate(_cubePrefab, PoolContainer);

        cube.LifetimeExpired += HandleCubeLifetimeExpired;

        return cube;
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
        FallingCube cube = GetObject(GetRandomSpawnPosition(), Random.rotation);

        cube.Initialize(_startColor, _minLifetime, _maxLifetime);
    }

    private void HandleCubeLifetimeExpired(FallingCube cube, Vector3 position)
    {
        _bombSpawner.Spawn(position);

        ReleaseObject(cube);
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
