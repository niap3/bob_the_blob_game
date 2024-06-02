using UnityEngine;

public class WaterScript : MonoBehaviour
{
public GameObject bob;

public PlayerInputSys controls;

private Vector2 move;

private void Awake() 
{
    controls = new PlayerInputSys();
    controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
    controls.Player.Move.canceled += ctx => move = Vector2.zero;
}

private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }

private void OnTriggerStay2D(Collider2D that)
{
    if (that.name == "Bob")
    {
        Rigidbody2D bobRb = bob.GetComponent<Rigidbody2D>();
        BobController bobController = bob.GetComponent<BobController>();

        bobRb.gravityScale = -5f;

        // movement
        bobRb.velocity = new Vector2(bobRb.velocity.x, move.y * bobController.walkSpeed);
    }
}

private void OnTriggerExit2D(Collider2D that)
{
    if (that.name == "Bob")
    {
        bob.GetComponent<Rigidbody2D>().gravityScale = 1f;
    }
}

}
