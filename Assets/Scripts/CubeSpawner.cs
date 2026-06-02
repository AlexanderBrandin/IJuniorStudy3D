using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeClickReader _clickReader;
    [SerializeField] private ExplodableCube _cubePrefab;

    [SerializeField] private int _minSpawnCount;
    [SerializeField] private int _maxSpawnCount;

    [SerializeField] private float _scaleMultiplier;
    [SerializeField] private float _splitChanceMultiplier;

    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _upwardsModifier;

    private void OnEnable()
    {
        _clickReader.CubeClicked += TrySplit;
    }

    private void OnDisable()
    {
        _clickReader.CubeClicked -= TrySplit;
    }

    private void TrySplit(ExplodableCube cube)
    {
        if (cube == null)
            return;

        Vector3 explosionPosition = cube.transform.position;

        if (Random.value <= cube.SplitChance)
        {
            List<Rigidbody> createdRigidbodies = SpawnCubes(cube);
            Explode(createdRigidbodies, explosionPosition);
        }

        Destroy(cube.gameObject);
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

            ExplodableCube newCube = Instantiate(
                _cubePrefab,
                spawnPosition,
                Random.rotation
            );

            newCube.transform.localScale = newScale;
            newCube.Initialize(newSplitChance);

            SetRandomColor(newCube);

            if (newCube.TryGetComponent(out Rigidbody rigidbody))
                createdRigidbodies.Add(rigidbody);
        }

        return createdRigidbodies;
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

    private void Explode(List<Rigidbody> rigidbodies, Vector3 explosionPosition)
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.AddExplosionForce(
                _explosionForce,
                explosionPosition,
                _explosionRadius,
                _upwardsModifier,
                ForceMode.Impulse
            );
        }
    }
}
