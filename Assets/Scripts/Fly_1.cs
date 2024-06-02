using UnityEngine;

public class Fly_1 : MonoBehaviour
{
public float maxFollowHeight = 10f; // Maximum height the object will follow the player
public float followSpeed = 0.3f; // Speed at which the object follows the player

void Update()
{
    GameObject player = GameObject.Find("Bob");
    
    if (player != null)
    {
        // Calculate the target position based on the player's position
        Vector3 targetPosition = new Vector2(player.transform.position.x, Mathf.Min(player.transform.position.y, maxFollowHeight));

        // Move towards the target position smoothly
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
}

