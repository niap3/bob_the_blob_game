using UnityEngine;

public class BobAnim : MonoBehaviour
{
    public Animator animator;
    public LayerMask groundLayer;
    public AudioSource jumpAudio;
    public AudioSource coinAudio;
    public PlayerInputSys controls;
    public GameObject player;
    private Vector2 move;

    private void Awake()
    {
        controls = new PlayerInputSys();

        controls.Player.Jump.started += ctx => player.GetComponent<BobController>().HandleJumping();

        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;
    }

    void Update()
    {
        bool isJumping = controls.Player.Jump.triggered;
        bool isGrounded = !isJumping;

        // Set animation parameters based on conditions
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isJump", isJumping);

        if (isJumping)
        {
            jumpAudio.Play();
        }

        // Flip
        if (move.x > 0)
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (move.x < 0)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
        }

        

    }
    
    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coinAudio.Play();
        }        
    }
}
