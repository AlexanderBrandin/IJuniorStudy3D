using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private ExplodableCube _cubePrefab;

    public ExplodableCube Spawn(Vector3 position, Quaternion rotation, Vector3 scale, float splitChance)
    {
        ExplodableCube cube = Instantiate(_cubePrefab, position, rotation);

        cube.transform.localScale = scale;
        cube.Initialize(splitChance);

        return cube;
    }

    public void Despawn(ExplodableCube cube)
    {
        if (cube == null)
            return;

        Destroy(cube.gameObject);
    }
}
