using UnityEngine;
using UnityEngine.InputSystem;

public class BobController : MonoBehaviour
{
    public PlayerInputSys controls;
    public Rigidbody2D rb;
    private float moveX;
    private Vector2 move;

    private void Awake()
    {
        controls = new PlayerInputSys();

        controls.Player.Jump.started += ctx => HandleJumping();

        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;
    }

    private void Update()
    {
        HandleInputs();
        IsGrounded();
        HandleWalking();
        HandleJumping();
    }

    public float playerMoveForce = 10f;

    private void HandleInputs()
    {
        moveX = move.x;

        float horizontalInput = move.x;
        float verticalInput = move.y;

        // Add player input force to the rigidbody
        Vector2 playerInputForce = new(horizontalInput, verticalInput);
        rb.AddForce(playerInputForce * playerMoveForce, ForceMode2D.Force);
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }

    #region Walk

    public float walkSpeed = 10;
    public float _acceleration = 2;
    public float currentMovementLerpSpeed = 100;

    public void HandleWalking()
    {
        var acceleration = IsGrounded() ? _acceleration : _acceleration * 0.5f;

        if (controls.Player.Move.IsPressed()) // Check if the horizontal input button is pressed
        {
            if (move.x < 0) // If the input is negative (left)
            {
                if (rb.velocity.x > 0) moveX = 0; // Immediate stop and turn. Just feels better
                moveX = -1;
            }
            else if (move.x > 0) // If the input is positive (right)
            {
                if (rb.velocity.x < 0) moveX = 0;
                moveX = 1;
            }
        }
        else // Handle continuous movement when the button is held
        {
            moveX = Mathf.MoveTowards(moveX, 0, acceleration * 2 * Time.deltaTime);
        }

        var idealVel = new Vector3(moveX * walkSpeed, rb.velocity.y);
        rb.velocity = Vector2.MoveTowards(rb.velocity, idealVel, currentMovementLerpSpeed * Time.deltaTime);
    }

    #endregion

    #region Grounding

    public LayerMask groundLayer;

    bool IsGrounded()
    {
        float raycastLength = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, groundLayer);

        if (hit.collider != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    #endregion


    #region Jump

    public float jumpForce = 15;
    public float fallMultiplier = 7;
    public float jumpVelocityFalloff = 8;
    public float coyoteTime = 0.1f;
    public int jumpsRemaining, maxJumps = 2;
    public float timeLeftGrounded = -10;

    public void HandleJumping()
    {
        if (controls.Player.Jump.triggered)
        {
            if (jumpsRemaining > 0 || (Time.time < timeLeftGrounded + coyoteTime))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpsRemaining--;
            }
        }

        if (rb.velocity.y < jumpVelocityFalloff || rb.velocity.y > 0 && !controls.Player.Jump.triggered)
        {
            rb.velocity += fallMultiplier * Physics.gravity.y * Time.deltaTime * Vector2.up;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsGrounded())
        {
            jumpsRemaining = maxJumps;
        }
    }

    #endregion

}