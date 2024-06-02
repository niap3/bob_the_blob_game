using UnityEngine;

public class WindScript : MonoBehaviour
{
[SerializeField] private float windForce = 50f;
[SerializeField] private WindDirection windDirection = WindDirection.None;

public enum WindDirection
{
    None,
    Up,
    Down,
    Right,
    Left
}

private void OnTriggerStay2D(Collider2D that)
{
    Vector2 windForceVector = Vector2.zero;

    switch (windDirection.ToString())
    {
        case "Up": windForceVector = Vector2.up * windForce; break;
        case "Down": windForceVector = Vector2.down * windForce; break;
        case "Right": windForceVector = Vector2.right * windForce; break;
        case "Left": windForceVector = Vector2.left * windForce; break;
        case "None": break;
    }

    that.attachedRigidbody.AddForce(windForceVector, ForceMode2D.Force);
}
}
