using UnityEngine;

public class CubeColorizer : MonoBehaviour
{
    public void ApplyRandomColor(Renderer renderer)
    {
        renderer.material.color = Random.ColorHSV();
    }

    public void ApplyColor(Renderer renderer, Color color)
    {
        renderer.material.color = color;
    }
}
