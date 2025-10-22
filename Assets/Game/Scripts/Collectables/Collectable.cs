using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableData collectable;
    private SpriteRenderer spriteRenderer;
    private Collider2D coll2D;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll2D = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            Collect(other.gameObject);
    }

    protected virtual void Collect(GameObject player)
    {
        if (collectable != null)
        {
            AudioManager.Instance.PlaySFX(collectable.soundEffect);
            collectable.Collect();
        }

        if (coll2D != null) coll2D.enabled = false;
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        Destroy(gameObject);
    }

}