using UnityEngine;

public class Collectable : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D coll2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll2D = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect(collision.gameObject);
        }
    }

    protected virtual void Collect(GameObject player)
    {
        if (coll2D != null) coll2D.enabled = false;
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
