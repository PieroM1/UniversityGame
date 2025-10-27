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
    private bool grounded;
    // Ground check parameters
    [SerializeField] private LayerMask isGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector3 boxDimensions;

    //Bomb Spawner
    private Transform bombSpawner;


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
    }

    private void Update()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        rb2D.linearVelocityX = move.x * speedX;
        if (move.x != 0)
        {
            sprite.flipX = move.x < 0;
        }
        if (jumpAction.WasPressedThisFrame()) Jump();
        // Animator parameters
        animator.SetFloat("Speed", Mathf.Abs(move.x));
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
        grounded = Physics2D.OverlapBox(groundCheck.position, boxDimensions, 0f, isGround);

        if (!jumped && grounded)
        {
            animator.SetBool("Jump", false);
            jumped = false;
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
            AudioManager.Instance.PlaySFX(SFXConstants.JUMP);
            rb2D.linearVelocityY = jumpForce;
            animator.SetBool("Jump", true);
            jumped = true;
        }
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