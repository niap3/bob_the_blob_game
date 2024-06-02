using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public float amplitude = 1f; // Amplitude of the oscillation
    public float frequency = 1f; // Frequency of the oscillation

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position based on sine function
        float newY = startPosition.y + amplitude * Mathf.Sin(Time.time * frequency);

        // Update the object's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}