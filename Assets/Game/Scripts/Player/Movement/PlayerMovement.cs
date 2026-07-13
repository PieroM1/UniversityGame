using System.Collections;
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
    private InputAction dashAction;
    //Movement parameters
    [SerializeField] private float speedX = 5f;
    [SerializeField] private float jumpForce = 7f;
    private short jumpCounter = 0;
    [SerializeField] private short maxJumpCount = 1;
    [SerializeField] private float extraJumpsHeight = 0.7f; //Jump height multiplier for extra jumps
    // Ground check parameters
    public bool grounded;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector3 boxDimensions;

    //Bomb Spawner
    private Transform bombSpawnPosition;

    //Dash Ability
    private bool canUseDashAbility = false;
    private const float defaultDashCooldown = 1f;
    private float dashCooldown;
    private float lastDashTime = -Mathf.Infinity;
    private const float defaultDashDistance = 3f;
    private float dashDistance;


    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        bombSpawnPosition = transform.Find("BombSpawnPosition");
        animator = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        dashAction = InputSystem.actions.FindAction("Dash");
        dashDistance = defaultDashDistance;
        dashCooldown = defaultDashCooldown;
    }

    private void Update()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        rb2D.linearVelocityX = move.x * speedX;
        if (move.x != 0)
        {
            int direction = (int)move.x;
            sprite.flipX = direction < 0;
            ChangePositionBombSpawner(direction);
        }
        if (jumpAction.WasPressedThisFrame() && grounded) Jump();
        else if (jumpAction.WasPressedThisFrame() && !grounded && maxJumpCount > 1) ExtraJump();
        if (dashAction.WasPressedThisFrame() && canUseDashAbility && Time.time >= lastDashTime + dashCooldown) Dash();
        // Animator parameters
        animator.SetInteger("SpeedX", (int)move.x);
        animator.SetFloat("SpeedY", rb2D.linearVelocityY);
        animator.SetBool("Grounded", grounded);
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapBox(groundCheck.position, boxDimensions, 0f, groundLayer);
        if (grounded)
        {
            animator.SetBool("ExtraJump", false);
            jumpCounter = 0;
        }
    }
    //Jump
    private void Jump()
    {
        jumpCounter++;
        AudioManager.Instance.PlaySFX(SFXConstants.JUMP);
        rb2D.linearVelocityY = jumpForce;
        animator.SetBool("ExtraJump", false);
    }
    //Extra Jump
    private void ExtraJump()
    {
        if (jumpCounter == 0) jumpCounter++;
        if (jumpCounter < maxJumpCount)
        {
            jumpCounter++;
            AudioManager.Instance.PlaySFX(SFXConstants.JUMP);
            rb2D.linearVelocityY = jumpForce * extraJumpsHeight;
            animator.SetBool("ExtraJump", true);
        }
    }
    public void EnableMultipleJumps(short maxJumpCount, float extraJumpsHeight)
    {
        this.maxJumpCount = maxJumpCount;
        this.extraJumpsHeight = extraJumpsHeight;
    }
    public void DisableMultipleJumps()
    {
        maxJumpCount = 1;
    }
    //Dash
    private void Dash()
    {
        AudioManager.Instance.PlaySFX(SFXConstants.DASH);
        animator.SetBool("Dash", true);
        StartCoroutine(DashMove());
        lastDashTime = Time.time;
    }

    private IEnumerator DashMove()
    {
        float dashDirection = sprite.flipX ? -1f : 1f;
        float dashEndTime = Time.time + 0.2f;

        while (Time.time < dashEndTime)
        {
            rb2D.MovePosition(rb2D.position + new Vector2(dashDistance * dashDirection * Time.fixedDeltaTime / 0.2f, 0f));
            yield return new WaitForFixedUpdate();
        }

        animator.SetBool("Dash", false);
    }
    public void EnableDashAbility(float dashDistance = defaultDashDistance, float dashCooldown = defaultDashCooldown)
    {
        this.dashDistance = dashDistance;
        canUseDashAbility = true;
    }
    public void DisableDashAbility()
    {
        this.dashDistance = defaultDashDistance;
        canUseDashAbility = false;
    }
    //Bomb
    private void ChangePositionBombSpawner(float newX)
    {
        if (bombSpawnPosition == null) return;

        Vector3 pos = bombSpawnPosition.localPosition;
        pos.x = newX;
        bombSpawnPosition.localPosition = pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, boxDimensions);
    }
}