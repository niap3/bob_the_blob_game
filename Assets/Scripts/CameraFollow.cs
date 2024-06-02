using UnityEngine;

public class CameraFollow : MonoBehaviour
{
[SerializeField] private Transform target;
[SerializeField] private float smoothSpeed = 0.125f;
[SerializeField] private float followDelay = 0.5f;
[SerializeField] private float minX = -3f;
[SerializeField] private float maxX = -3f;
[SerializeField] private float fallMultiplier = 3.5f;
[SerializeField] private Vector3 cameraOffset;
private Vector3 velocity = Vector3.zero;

void Start()
{
    //target = GameObject.Find("Bob").transform;

    if (target != null)
    {
        cameraOffset = transform.position - target.position;
    }
}

void FixedUpdate()
{
    if (target != null)
    {
        Vector3 targetPosition = target.position + cameraOffset;
        Vector3 clampedPosition = new(Mathf.Clamp(targetPosition.x, minX, maxX), targetPosition.y, targetPosition.z);

        //check this
        transform.position = Vector3.SmoothDamp(transform.position, clampedPosition, ref velocity, smoothSpeed, Mathf.Infinity, followDelay * Time.fixedDeltaTime);
        //transform.position = Vector3.Lerp(transform.position, clampedPosition, Time.fixedDeltaTime);

        // Check if the player is above or below the camera view
        float playerScreenY = Camera.main.WorldToScreenPoint(target.position).y;
        bool isBelowCamera = playerScreenY < 0 || playerScreenY > Screen.height;

        // Fall faster when the player is falling and out of camera view
        if (isBelowCamera && target.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            Vector3 targetFallPosition = transform.position + Vector3.up * target.GetComponent<Rigidbody2D>().velocity.y * fallMultiplier * Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(transform.position, targetFallPosition, fallMultiplier * Time.fixedDeltaTime);
        }
    }
}
}
