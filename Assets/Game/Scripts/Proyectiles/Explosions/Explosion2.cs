using UnityEngine;
using System.Collections;

public class Explosion2 : MonoBehaviour
{
    [SerializeField] private float radius = 3;
    [SerializeField] private float power = 800;
    [SerializeField] private float delaySeconds = 3f;

    private void OnEnable()
    {
        StartCoroutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(delaySeconds);
        ExplodeObject();
        Destroy(gameObject); // opcional: destruye la bomba tras explotar
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
}
