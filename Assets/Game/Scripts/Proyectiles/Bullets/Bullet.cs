using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private float daño = 10f;
    [SerializeField] private float tiempoVida = 3f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    public void Lanzar(Vector2 direccion)
    {
        rb.linearVelocity = direccion.normalized * velocidad;
    }

    public float GetDaño() => daño;
}
