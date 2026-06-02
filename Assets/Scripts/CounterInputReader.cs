using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CounterInputReader : MonoBehaviour
{
    public event Action Clicked;

    private void Update()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
            Clicked?.Invoke();
    }
}
