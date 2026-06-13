using UnityEngine;

public class ExplosionForceApplier : MonoBehaviour
{
    public void Explode(Vector3 position, float radius, float force, Rigidbody ignoredRigidbody)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.attachedRigidbody;

            if (rigidbody == null)
                continue;

            if (rigidbody == ignoredRigidbody)
                continue;

            rigidbody.AddExplosionForce(force, position, radius, 0f, ForceMode.Impulse);
        }
    }
}
