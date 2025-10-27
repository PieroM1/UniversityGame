using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Component references
    private Animator animator;
    private Rigidbody2D rb2D;
    private SpriteRenderer sprite;
    private InputAction moveAction;
    private InputAction jumpAction;
    //Movement parameters
    [SerializeField] private float speedX = 5f;
    [SerializeField] private float jumpForce = 7f;
    private bool jumped = false;
    private short jumpCounter = 0;
    [SerializeField] private short maxJumpCount = 1;
    private bool canJumpMultipleTimes;
    [SerializeField] private float extraJumpsHeight = 0.7f; //Jump height multiplier for extra jumps
    private bool grounded;
    // Ground check parameters
    [SerializeField] private LayerMask isGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector3 boxDimensions;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        rb2D.linearVelocityX = move.x * speedX;
        if (move.x != 0)
        {
            sprite.flipX = move.x < 0;
        }
        if (jumpAction.WasPressedThisFrame() && grounded) Jump();
        else if (jumpAction.WasPressedThisFrame() && !grounded && canJumpMultipleTimes) DoubleJump();

        // Animator parameters
        animator.SetFloat("Speed", Mathf.Abs(move.x));
        animator.SetBool("Grounded", grounded);
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapBox(groundCheck.position, boxDimensions, 0f, isGround);

        if (!jumped && grounded)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("ExtraJump", false);
            jumped = false;
            jumpCounter = 0;
        }
        else if (!jumped && !grounded)
        {
            jumped = false;
        }
        else jumped = grounded;
    }

    private void Jump()
    {
        if (grounded)
        {
            jumpCounter++;
            AudioManager.Instance.PlaySFX(SFXConstants.JUMP);
            rb2D.linearVelocityY = jumpForce;
            animator.SetBool("Jump", true);
            jumped = true;
        }
    }

    private void DoubleJump()
    {
        if (jumpCounter == 0) jumpCounter++;
        if (jumpCounter < maxJumpCount)
        {
            jumpCounter++;
            AudioManager.Instance.PlaySFX(SFXConstants.JUMP);
            rb2D.linearVelocityY = jumpForce * extraJumpsHeight;
            animator.SetBool("ExtraJump", true);
            animator.SetBool("Jump", false);
        }
    }

    public void EnableMultipleJumps(short maxJumpCount, float extraJumpsHeight)
    {
        canJumpMultipleTimes = true;
        this.maxJumpCount = maxJumpCount;
        this.extraJumpsHeight = extraJumpsHeight;
    }

    public void DisableMultipleJumps()
    {
        canJumpMultipleTimes = false;
        maxJumpCount = 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, boxDimensions);
    }
}