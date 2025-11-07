using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f;
    [SerializeField] private float delaySeconds = 4f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private IEnumerator AnimateExplosion()
    {
        yield return new WaitForSeconds(delaySeconds - 0.5f);
        animator.SetBool("Exploded", true);
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(delaySeconds);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
        }
    }
}
