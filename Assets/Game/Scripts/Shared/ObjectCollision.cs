using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D col2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero") || collision.gameObject.CompareTag("Bullet"))
        {
            if (gameObject.CompareTag("Enemy"))
            {
                //GameController.instance.PlayerHit();
            }
            if (gameObject.CompareTag("PowerUp"))
            {
                //GameController.instance.GainLight();
            }
            if (gameObject.CompareTag("Collectable"))
            {
                //GameController.instance.UpdateScore();
            }

            if (col2D != null) col2D.enabled = false;

            if (spriteRenderer != null) spriteRenderer.enabled = false;

            col2D.enabled = false;
            spriteRenderer.enabled = false;
        }
    }
}
