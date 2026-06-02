using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public event Action<int> ValueChanged;

    [SerializeField] private CounterInputReader _inputReader;
    [SerializeField] private float _delay;
    [SerializeField] private int _incrementValue;

    private int _value;
    private Coroutine _countingCoroutine;

    private void OnEnable()
    {
        _inputReader.Clicked += ToggleCounting;
    }

    private void Start()
    {
        ValueChanged?.Invoke(_value);
    }

    private void OnDisable()
    {
        _inputReader.Clicked -= ToggleCounting;
        StopCounting();
    }

    private void ToggleCounting()
    {
        if (_countingCoroutine == null)
            StartCounting();
        else
            StopCounting();
    }

    private void StartCounting()
    {
        _countingCoroutine = StartCoroutine(Counting());
    }

    private void StopCounting()
    {
        if (_countingCoroutine == null)
            return;

        StopCoroutine(_countingCoroutine);
        _countingCoroutine = null;
    }

    private IEnumerator Counting()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_delay);

            _value += _incrementValue;
            ValueChanged?.Invoke(_value);
        }
    }
}
