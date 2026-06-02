using TMPro;
using UnityEngine;

public class CounterView : MonoBehaviour
{
    [SerializeField] private Counter _counter;
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private string _counterPrefix;

    private void OnEnable()
    {
        _counter.ValueChanged += Display;
    }

    private void OnDisable()
    {
        _counter.ValueChanged -= Display;
    }

    private void Display(int value)
    {
        _counterText.text = $"{_counterPrefix}{value}";
    }
}
