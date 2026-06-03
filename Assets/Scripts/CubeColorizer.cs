using UnityEngine;

public class CubeColorizer : MonoBehaviour
{
    public void ApplyRandomColor(ExplodableCube cube)
    {
        if (cube == null)
            return;

        if (cube.TryGetComponent(out Renderer renderer))
            renderer.material.color = Random.ColorHSV();
    }
}
