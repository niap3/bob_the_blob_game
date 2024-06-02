using UnityEngine;

public class PlatformController : MonoBehaviour
{
public enum MovementType
{
    None,
    Rotation,
    VerticalMovement,
    HorizontalMovement,
}
public MovementType movementType;

// Rotation Variables
public float rotationSpeed = 30f;

// Vertical Movement Variables
public float verticalMoveDistance = 5f;
public float verticalMoveSpeed = 2f;
private Vector3 verticalInitialPosition;

// Horizontal Movement Variables
public float horizontalMoveDistance = 5f;
public float horizontalMoveSpeed = 2f;
private Vector3 horizontalInitialPosition;

void Start()
{
    // Store the initial positions
    verticalInitialPosition = transform.position;
    horizontalInitialPosition = transform.position;
}


void Update()
{
    switch (movementType)
    {
        case MovementType.Rotation:
            RotatePlatform();
            break;

        case MovementType.VerticalMovement:
            MoveVertically();
            break;

        case MovementType.HorizontalMovement:
            MoveHorizontally();
            break;

        case MovementType.None:
            break;
    }
}

void RotatePlatform()
{
    transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
}

void MoveVertically()
{
    float t = Mathf.PingPong(Time.time * verticalMoveSpeed / verticalMoveDistance, 1f);
    float easedT = EaseInOut(t);
    float verticalPosition = Mathf.Lerp(verticalInitialPosition.y, verticalInitialPosition.y + verticalMoveDistance, easedT);

    transform.position = new Vector3(transform.position.x, verticalPosition, transform.position.z);
}

void MoveHorizontally()
{
    float t = Mathf.PingPong(Time.time * horizontalMoveSpeed / horizontalMoveDistance, 1f);
    float easedT = EaseInOut(t);
    float horizontalPosition = Mathf.Lerp(horizontalInitialPosition.x, horizontalInitialPosition.x + horizontalMoveDistance, easedT);

    transform.position = new Vector3(horizontalPosition, transform.position.y, transform.position.z);
}

float EaseInOut(float t) => t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;

}
