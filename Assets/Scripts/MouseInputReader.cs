using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInputReader : MonoBehaviour
{
    public event Action<Vector2> Clicked;

    private void Update()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
            Clicked?.Invoke(Mouse.current.position.ReadValue());
    }
}
