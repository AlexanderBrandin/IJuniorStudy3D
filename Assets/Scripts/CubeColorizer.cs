using UnityEngine;

public class CubeColorizer : MonoBehaviour
{
    public void ApplyRandomColor(ExplodableCube cube)
    {
        Renderer renderer = cube.GetComponent<Renderer>();
        renderer.material.color = Random.ColorHSV();
    }
}
