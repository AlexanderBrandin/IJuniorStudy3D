using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class Bomb : MonoBehaviour
{
    public event Action<Bomb, Vector3> Exploded;

    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private Material _material;
    private Coroutine _fadingCoroutine;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
    }

    private void OnDisable()
    {
        if (_fadingCoroutine != null)
            StopCoroutine(_fadingCoroutine);

        _fadingCoroutine = null;
    }

    public void Initialize(float lifetime)
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        SetAlpha(1f);

        _fadingCoroutine = StartCoroutine(Fading(lifetime));
    }

    private IEnumerator Fading(float lifetime)
    {
        float elapsedTime = 0f;

        while (elapsedTime < lifetime)
        {
            elapsedTime += Time.deltaTime;

            float alpha = 1f - elapsedTime / lifetime;

            SetAlpha(alpha);

            yield return null;
        }

        Exploded?.Invoke(this, transform.position);
    }

    private void SetAlpha(float alpha)
    {
        Color color = _material.color;
        color.a = alpha;
        _material.color = color;
    }
}
