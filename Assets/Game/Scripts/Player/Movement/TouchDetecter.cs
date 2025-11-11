using UnityEngine;

public class TouchDetecter : MonoBehaviour
{

    private Explosion2 script;
    [SerializeField] private float bounceForce = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        script = GetComponentInParent<Explosion2>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Aplastando la bomba");
            HandlePlayerBounce(collision.gameObject);
        }

    }
    
    private void HandlePlayerBounce(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            script.ExplodeTouch();
        }
    }
}
