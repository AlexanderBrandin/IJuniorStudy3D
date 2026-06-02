using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeClickReader : MonoBehaviour
{
    public event Action<ExplodableCube> CubeClicked;

    [SerializeField] private Camera _camera;
    [SerializeField] private float _rayDistance;

    private void Update()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
            TryClick();
    }

    private void TryClick()
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance) == false)
            return;

        if (hit.collider.TryGetComponent(out ExplodableCube cube))
            CubeClicked?.Invoke(cube);
    }
}
