using UnityEngine;
using UnityEngine.InputSystem;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float radius = 3;
    [SerializeField] private float power = 800;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ExplodeObject();
        }
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
