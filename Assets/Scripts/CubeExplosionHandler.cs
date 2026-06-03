using System.Collections.Generic;
using UnityEngine;

public class CubeExplosionHandler : MonoBehaviour
{
    [SerializeField] private MouseInputReader _inputReader;
    [SerializeField] private CubeRaycaster _raycaster;
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private CubeColorizer _colorizer;
    [SerializeField] private CubeExploder _exploder;

    [SerializeField] private int _minSpawnCount;
    [SerializeField] private int _maxSpawnCount;
    [SerializeField] private float _scaleMultiplier;
    [SerializeField] private float _splitChanceMultiplier;
    [SerializeField] private float _spawnRadius;

    private void OnEnable()
    {
        _inputReader.Clicked += HandleClicked;
    }

    private void OnDisable()
    {
        _inputReader.Clicked -= HandleClicked;
    }

    private void HandleClicked(Vector2 screenPosition)
    {
        if (_raycaster.TryGetCube(screenPosition, out ExplodableCube cube) == false)
            return;

        ExplodeCube(cube);
    }

    private void ExplodeCube(ExplodableCube cube)
    {
        Vector3 explosionPosition = cube.transform.position;

        if (CanSplit(cube))
        {
            List<Rigidbody> createdRigidbodies = SpawnCubes(cube);
            _exploder.Explode(createdRigidbodies, explosionPosition);
        }

        _spawner.Despawn(cube);
    }

    private bool CanSplit(ExplodableCube cube)
    {
        return Random.value <= cube.SplitChance;
    }

    private List<Rigidbody> SpawnCubes(ExplodableCube parentCube)
    {
        List<Rigidbody> createdRigidbodies = new List<Rigidbody>();

        int spawnCount = Random.Range(_minSpawnCount, _maxSpawnCount + 1);
        Vector3 newScale = parentCube.transform.localScale * _scaleMultiplier;
        float newSplitChance = parentCube.SplitChance * _splitChanceMultiplier;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition(parentCube.transform.position);
            Quaternion rotation = Random.rotation;

            ExplodableCube createdCube = _spawner.Spawn(
                spawnPosition,
                rotation,
                newScale,
                newSplitChance
            );

            _colorizer.SetRandomColor(createdCube);

            if (createdCube.TryGetComponent(out Rigidbody rigidbody))
                createdRigidbodies.Add(rigidbody);
        }

        return createdRigidbodies;
    }

    private Vector3 GetSpawnPosition(Vector3 center)
    {
        return center + Random.insideUnitSphere * _spawnRadius;
    }
}
