using UnityEngine;

public class Collectable : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D coll2D;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect(collision.gameObject);
        }   
    }

    protected virtual void Collect(GameObject player)
    {
        Debug.Log("Collectable collected by " + player.name);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
