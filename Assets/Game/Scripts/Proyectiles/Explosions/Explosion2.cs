using UnityEngine;
using System.Collections;

public class Explosion2 : MonoBehaviour
{
    [SerializeField] private float radius = 3;
    [SerializeField] private float power = 800;
    [SerializeField] private float delaySeconds = 4f;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(AnimateExplosion());
        StartCoroutine(ExplodeAfterDelay());
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

    void ExplodeObject()
    {
        Vector2 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb2D = collider.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                rb2D.AddExplosionForce(power, explosionPos, radius);
            }
        }
    }

    public void ExplodeTouch()
    {
        animator.SetBool("Exploded", true);
        Destroy(gameObject);
    }
}
