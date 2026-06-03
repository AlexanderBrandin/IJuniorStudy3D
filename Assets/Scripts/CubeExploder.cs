using System.Collections.Generic;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _upwardsModifier;

    public void Explode(IEnumerable<Rigidbody> rigidbodies, Vector3 explosionPosition)
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            if (rigidbody == null)
                continue;

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
