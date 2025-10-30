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
    private InputAction attackAction;
    //Movement parameters
    [SerializeField] private float speedX = 5f;
    [SerializeField] private float jumpForce = 7f;
    private short jumpCounter = 0;
    [SerializeField] private short maxJumpCount = 1;
    [SerializeField] private float extraJumpsHeight = 0.7f; //Jump height multiplier for extra jumps
    // Ground check parameters
    private bool grounded;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector3 boxDimensions;

    //Bomb Spawner
    private Transform bombSpawner;

    //Attack Abilitity
    private bool canUseAttackAbility = false;

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
        bombSpawner = transform.Find("BombSpawner");
        animator = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        dashAction = InputSystem.actions.FindAction("Dash");
        attackAction = InputSystem.actions.FindAction("Attack");
        dashDistance = defaultDashDistance;
        dashCooldown = defaultDashCooldown;
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
        else if (jumpAction.WasPressedThisFrame() && !grounded && maxJumpCount > 1) DoubleJump();
        if (dashAction.WasPressedThisFrame() && canUseDashAbility && Time.time >= lastDashTime + dashCooldown) Dash();
        // Animator parameters
        animator.SetInteger("SpeedX", (int)move.x);
        animator.SetFloat("SpeedY", rb2D.linearVelocityY);
        animator.SetBool("Grounded", grounded);

        if (move.x > 0)
        {
            sprite.flipX = false;
            ChangePositionBombSpawner(1f);
        }
        // Si se mueve a la izquierda
        else if (move.x < 0)
        {
            sprite.flipX = true;
            ChangePositionBombSpawner(-1f);
        }
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

    private void Jump()
    {
        jumpCounter++;
        AudioManager.Instance.PlaySFX(SFXConstants.JUMP);
        rb2D.linearVelocityY = jumpForce;
        animator.SetBool("ExtraJump", false);
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

    public void EnableAttackAbility()
    {
        canUseAttackAbility = true;
    }
    public void DisableAttackAbility()
    {
        canUseAttackAbility = false;
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
            rb2D.position += new Vector2(dashDistance * dashDirection * Time.deltaTime / 0.2f, 0f);
            yield return null;
        }
        animator.SetBool("Dash", false);
    }

    private void ChangePositionBombSpawner(float newX)
    {
        if (bombSpawner == null) return;

        Vector3 pos = bombSpawner.localPosition;
        pos.x = newX;
        bombSpawner.localPosition = pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, boxDimensions);
    }
}