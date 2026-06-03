using UnityEngine;

public class CubeRaycaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _rayDistance;

    public bool TryGetCube(Vector2 screenPosition, out ExplodableCube cube)
    {
        cube = null;

        Ray ray = _camera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance) == false)
            return false;

        return hit.collider.TryGetComponent(out cube);
    }
}
