using System.Collections.Generic;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    [SerializeField] private float _baseExplosionForce;
    [SerializeField] private float _baseExplosionRadius;
    [SerializeField] private float _upwardsModifier;

    public void Explode(IEnumerable<Rigidbody> rigidbodies, Vector3 explosionPosition, float sourceCubeSize)
    {
        float explosionForce = GetExplosionForce(sourceCubeSize);
        float explosionRadius = GetExplosionRadius(sourceCubeSize);

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            Explode(rigidbody, explosionPosition, explosionForce, explosionRadius);
        }
    }

    public void ExplodeAround(Vector3 explosionPosition, float sourceCubeSize, ExplodableCube ignoredCube)
    {
        float explosionForce = GetExplosionForce(sourceCubeSize);
        float explosionRadius = GetExplosionRadius(sourceCubeSize);

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out ExplodableCube cube) == false)
                continue;

            if (cube == ignoredCube)
                continue;

            Rigidbody rigidbody = cube.GetComponent<Rigidbody>();

            Explode(rigidbody, explosionPosition, explosionForce, explosionRadius);
        }
    }

    private void Explode(Rigidbody rigidbody, Vector3 explosionPosition, float explosionForce, float explosionRadius)
    {
        rigidbody.AddExplosionForce(
            explosionForce,
            explosionPosition,
            explosionRadius,
            _upwardsModifier,
            ForceMode.Impulse
        );
    }

    private float GetExplosionForce(float sourceCubeSize)
    {
        return _baseExplosionForce / sourceCubeSize;
    }

    private float GetExplosionRadius(float sourceCubeSize)
    {
        return _baseExplosionRadius / sourceCubeSize;
    }
}
