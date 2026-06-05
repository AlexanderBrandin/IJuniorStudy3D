using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class FallingCube : MonoBehaviour
{
    public event Action<FallingCube> LifeTimeExpired;

    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;

    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private CubeColorizer _colorizer;
    private Color _startColor;
    private bool _hasTouchedPlatform;
    private Coroutine _lifeTimeCoroutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    public void Initialize(CubeColorizer colorizer, Color startColor)
    {
        _colorizer = colorizer;
        _startColor = startColor;

        ResetState();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform)
            return;

        if (collision.collider.TryGetComponent(out Platform platform) == false)
            return;

        _hasTouchedPlatform = true;
        _colorizer.ApplyRandomColor(_renderer);

        StartLifeTimeCountdown();
    }

    private void ResetState()
    {
        _hasTouchedPlatform = false;

        if (_lifeTimeCoroutine != null)
            StopCoroutine(_lifeTimeCoroutine);

        _lifeTimeCoroutine = null;

        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _colorizer.ApplyColor(_renderer, _startColor);
    }

    private void StartLifeTimeCountdown()
    {
        _lifeTimeCoroutine = StartCoroutine(LifeTimeCountdown());
    }

    private IEnumerator LifeTimeCountdown()
    {
        float lifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);

        yield return new WaitForSeconds(lifeTime);

        LifeTimeExpired?.Invoke(this);
    }
}
