using UnityEngine;

public class TouchDetecter : MonoBehaviour
{

    private Explosion2 script;
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
            script.ExplodeTouch();
        }
        
    }
}
