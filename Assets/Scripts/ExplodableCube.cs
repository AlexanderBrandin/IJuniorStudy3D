using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]
public class ExplodableCube : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _splitChance;

    public float SplitChance => _splitChance;

    public void Initialize(float splitChance)
    {
        _splitChance = splitChance;
    }
}
