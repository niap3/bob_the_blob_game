using UnityEngine;

public class StickyPlat : MonoBehaviour
{

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

private void OnTriggerStay2D(Collider2D collision)
{
    if (collision.name == "Bob")
    {
        if (move.x != 0)
        {
            collision.transform.SetParent(null);
        }
        else
        {
            collision.transform.SetParent(transform);
        }
    }
}

private void OnTriggerExit2D(Collider2D collision)
{
    collision.transform.SetParent(null);
}
}