using UnityEngine;

public class Movimiento2D : MonoBehaviour
{
    [SerializeField] private Controles controles;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    private bool enSuelo;
    private Animator animator;
    private Vector2 direccion;
    private bool mirandoDerecha = true;

    private bool haSaltado = false;

    private void Awake()
    {
        controles ??= new Controles();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controles.Enable();
        controles.Movimiento.Saltar.started += _ => Saltar();
    }

    private void OnDisable()
    {
        controles.Disable();
        controles.Movimiento.Saltar.started -= _ => Saltar();
    }

    private void Update()
    {
        direccion = controles.Movimiento.Mover.ReadValue<Vector2>();
        AjustarRotacion(direccion.x);

        // Parámetros para el Animator
        animator.SetFloat("Velocidad", Mathf.Abs(direccion.x));
        animator.SetBool("EnSuelo", enSuelo);
    }

    private void FixedUpdate()
    {
        rb2D.linearVelocity = new Vector2(direccion.x * velocidadMovimiento, rb2D.linearVelocityY);
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);

        if (!haSaltado && enSuelo)
        {
            animator.SetBool("Salto", false);
            haSaltado = false;
        }
        else if (!haSaltado && !enSuelo)
        {
            haSaltado = false;
        }
        else haSaltado = enSuelo;
    }

    private void AjustarRotacion(float direccionX)
    {
        if ((direccionX > 0 && !mirandoDerecha) || (direccionX < 0 && mirandoDerecha))
            Girar();
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void Saltar()
    {
        if (enSuelo)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocityX, fuerzaSalto);
            animator.SetBool("Salto", true);
            haSaltado = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}