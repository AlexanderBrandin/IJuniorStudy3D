using System.Collections.Generic;
using UnityEngine;

public class CubeExplosionHandler : MonoBehaviour
{
    [SerializeField] private CubeClickReader _clickReader;
    [SerializeField] private CubeSpawner _spawner;

    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _upwardsModifier;

    private void OnEnable()
    {
        _clickReader.CubeClicked += HandleCubeClicked;
    }

    private void OnDisable()
    {
        _clickReader.CubeClicked -= HandleCubeClicked;
    }

    private void HandleCubeClicked(ExplodableCube cube)
    {
        if (cube == null)
            return;

        Vector3 explosionPosition = cube.transform.position;

        if (CanSplit(cube))
        {
            List<Rigidbody> createdRigidbodies = _spawner.SpawnFrom(cube);
            Explode(createdRigidbodies, explosionPosition);
        }

        Destroy(cube.gameObject);
    }

    private bool CanSplit(ExplodableCube cube)
    {
        return Random.value <= cube.SplitChance;
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
