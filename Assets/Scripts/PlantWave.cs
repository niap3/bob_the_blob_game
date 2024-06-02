using UnityEngine;

public class PlantWave : MonoBehaviour
{
    private float rotationSpeed = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        rotationSpeed = 30f;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        rotationSpeed = 0f;
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * 3f * Time.deltaTime);
    }
}