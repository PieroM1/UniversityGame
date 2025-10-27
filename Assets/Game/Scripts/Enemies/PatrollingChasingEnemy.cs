using UnityEngine;

public class PatrollingChasingEnemy : MonoBehaviour
{
      private enum State { Patrol, Chase }

    [Header("Movimiento")]
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private Transform limit1;
    [SerializeField] private Transform limit2;

    [Header("Detección (solo distancia)")]
    [SerializeField] private Transform player;          // Asignar o tag "Player"
    [SerializeField] private float detectionRange = 5f; // Radio de detección (mundo)

    private float leftX, rightX;   // límites en mundo
    private int dir = 1;           // -1 izquierda, 1 derecha
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private State state = State.Patrol;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Límites en espacio de mundo (¡clave!)
        leftX  = Mathf.Min(limit1.position.x, limit2.position.x);
        rightX = Mathf.Max(limit1.position.x, limit2.position.x);

        // Autodetectar jugador si no se asignó
        if (!player)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p) player = p.transform;
        }
    }

    private void Update()
    {
        if (!player) { state = State.Patrol; return; }

        float distance = Vector2.Distance(transform.position, player.position);
        state = (distance <= detectionRange) ? State.Chase : State.Patrol;
    }

    private void FixedUpdate()
    {
        if (state == State.Chase) ChaseWithinLimits();
        else Patrol();

        // Volteo del sprite según la velocidad
        sprite.flipX = rb.linearVelocity.x < 0f;
    }

    private void Patrol()
    {
        float x = transform.position.x;

        // Rebotar en los límites (mundo)
        if ((x <= leftX && dir < 0) || (x >= rightX && dir > 0))
            dir *= -1;

        rb.linearVelocity = new Vector2(dir * patrolSpeed, rb.linearVelocity.y);
    }

    private void ChaseWithinLimits()
    {
        if (!player) return;

        float enemyX  = transform.position.x;
        float playerX = player.position.x;

        // Objetivo clampeado a la zona permitida
        float targetX = Mathf.Clamp(playerX, leftX, rightX);

        float delta = targetX - enemyX;
        if (Mathf.Abs(delta) < 0.01f)
        {
            // Ya está en el borde o encima del objetivo: quedar quieto en X
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        int chaseDir = delta > 0 ? 1 : -1;

        // Evitar que “se escape” por física: si está en el borde y empuja hacia afuera, no mover
        bool pushingOutside =
            (enemyX <= leftX  && chaseDir < 0) ||
            (enemyX >= rightX && chaseDir > 0);

        rb.linearVelocity = new Vector2(pushingOutside ? 0f : chaseDir * chaseSpeed, rb.linearVelocity.y);

        // Recordar dirección para retomar patrulla naturalmente
        if (!pushingOutside) dir = chaseDir;
    }

    private void OnDrawGizmosSelected()
    {
        // Detección
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Límites
        if (limit1 && limit2)
        {
            Gizmos.color = Color.cyan;
            Vector3 a = limit1.position, b = limit2.position;
            Gizmos.DrawLine(new Vector3(a.x, a.y - 0.5f, 0), new Vector3(a.x, a.y + 0.5f, 0));
            Gizmos.DrawLine(new Vector3(b.x, b.y - 0.5f, 0), new Vector3(b.x, b.y + 0.5f, 0));
        }
    }
}