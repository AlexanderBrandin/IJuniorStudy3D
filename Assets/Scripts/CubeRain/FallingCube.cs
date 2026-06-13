using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class FallingCube : MonoBehaviour
{
    public event Action<FallingCube, Vector3> LifetimeExpired;

    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private Coroutine _lifetimeCoroutine;
    private bool _hasTouchedPlatform;
    private float _minLifetime;
    private float _maxLifetime;
    private Color _startColor;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnDisable()
    {
        if (_lifetimeCoroutine != null)
            StopCoroutine(_lifetimeCoroutine);

        _lifetimeCoroutine = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform)
            return;

        if (collision.collider.TryGetComponent(out Platform _) == false)
            return;

        _hasTouchedPlatform = true;
        ApplyRandomColor();

        float lifetime = UnityEngine.Random.Range(_minLifetime, _maxLifetime);
        _lifetimeCoroutine = StartCoroutine(CountingLifetime(lifetime));
    }

    public void Initialize(Color startColor, float minLifetime, float maxLifetime)
    {
        _startColor = startColor;
        _minLifetime = minLifetime;
        _maxLifetime = maxLifetime;

        _hasTouchedPlatform = false;
        _renderer.material.color = _startColor;

        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private IEnumerator CountingLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);

        LifetimeExpired?.Invoke(this, transform.position);
    }

    private void ApplyRandomColor()
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV();
    }
}
