using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    [SerializeField] private float speedX = 2f;
    [SerializeField] private Transform limit1;
    [SerializeField] private Transform limit2;

    private float limitLeft;
    private float limitRight;
    private Rigidbody2D body;
    private SpriteRenderer sprite;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Tomamos los límites en espacio local una sola vez
        limitLeft = Mathf.Min(limit1.localPosition.x, limit2.localPosition.x);
        limitRight = Mathf.Max(limit1.localPosition.x, limit2.localPosition.x);
    }

    private void Update()
    {
        float posX = transform.localPosition.x;

        // Invertir dirección al llegar a los límites
        if ((posX < limitLeft && speedX < 0) || (posX > limitRight && speedX > 0))
            speedX = -speedX;
        // Movimiento horizontal
        body.linearVelocityX = speedX;
        // Voltear sprite
        sprite.flipX = speedX < 0;
    }
}