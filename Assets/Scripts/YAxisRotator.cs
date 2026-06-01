using UnityEngine;

public class YAxisRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
