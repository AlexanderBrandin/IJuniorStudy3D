using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Counter : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private float _delay;
    [SerializeField] private int _incrementValue;
    [SerializeField] private string _counterPrefix;

    private int _value;
    private Coroutine _countingCoroutine;

    private void Awake()
    {
        UpdateCounterText();
    }

    private void Update()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
            ToggleCounting();
    }

    private void OnDisable()
    {
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
            UpdateCounterText();
        }
    }

    private void UpdateCounterText()
    {
        if (_counterText == null)
            return;

        _counterText.text = $"{_counterPrefix}{_value}";
    }
}
