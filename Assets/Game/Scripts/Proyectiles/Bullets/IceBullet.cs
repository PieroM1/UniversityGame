using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class IceBullet : MonoBehaviour
{
    public float speed = 10f;

    private SpriteRenderer spriteRenderer;
    private Collider2D col2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (col2D != null) col2D.enabled = false;
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        col2D.enabled = false;
        spriteRenderer.enabled = false;
    }
}
