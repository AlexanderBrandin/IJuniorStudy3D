using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private ExplodableCube _cubePrefab;
    [SerializeField] private int _minSpawnCount;
    [SerializeField] private int _maxSpawnCount;
    [SerializeField] private float _scaleMultiplier;
    [SerializeField] private float _splitChanceMultiplier;
    [SerializeField] private float _spawnRadius;

    public List<Rigidbody> SpawnFrom(ExplodableCube parentCube)
    {
        List<Rigidbody> createdRigidbodies = new List<Rigidbody>();

        int spawnCount = Random.Range(_minSpawnCount, _maxSpawnCount + 1);
        Vector3 newScale = parentCube.transform.localScale * _scaleMultiplier;
        float newSplitChance = parentCube.SplitChance * _splitChanceMultiplier;

        for (int i = 0; i < spawnCount; i++)
        {
            ExplodableCube newCube = CreateCube(parentCube.transform.position, newScale, newSplitChance);

            if (newCube.TryGetComponent(out Rigidbody rigidbody))
                createdRigidbodies.Add(rigidbody);
        }

        return createdRigidbodies;
    }

    private ExplodableCube CreateCube(Vector3 parentPosition, Vector3 scale, float splitChance)
    {
        Vector3 spawnPosition = GetSpawnPosition(parentPosition);

        ExplodableCube cube = Instantiate(
            _cubePrefab,
            spawnPosition,
            Random.rotation
        );

        cube.transform.localScale = scale;
        cube.Initialize(splitChance);
        SetRandomColor(cube);

        return cube;
    }

    private Vector3 GetSpawnPosition(Vector3 center)
    {
        return center + Random.insideUnitSphere * _spawnRadius;
    }

    private void SetRandomColor(ExplodableCube cube)
    {
        if (cube.TryGetComponent(out Renderer renderer))
            renderer.material.color = Random.ColorHSV();
    }
}
