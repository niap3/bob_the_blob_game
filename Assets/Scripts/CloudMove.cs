using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float horizontalMoveDistance = 5f;
    public float horizontalMoveSpeed = 2f;
    private Vector3 horizontalInitialPosition;

    void Start()
    {
        // Store the initial positions
        horizontalInitialPosition = transform.position;
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time * horizontalMoveSpeed / horizontalMoveDistance, 1f);
        float easedT = EaseInOut(t);
        float horizontalPosition = Mathf.Lerp(horizontalInitialPosition.x, horizontalInitialPosition.x + horizontalMoveDistance, easedT);

        transform.position = new Vector3(horizontalPosition, transform.position.y, transform.position.z);
    }

    float EaseInOut(float t) => t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;
}