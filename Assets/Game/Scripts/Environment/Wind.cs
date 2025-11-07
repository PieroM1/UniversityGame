using UnityEngine;
using System.Collections;
public class Wind : MonoBehaviour
{
    public AreaEffector2D effector;
    public SpriteRenderer spriteRenderer;
    public float tiempoEncendido = 1f;
    public float tiempoApagado = 3f;

    void Start()
    {
        if (effector == null)
            effector = GetComponent<AreaEffector2D>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(Ciclo());
    }

    System.Collections.IEnumerator Ciclo()
    {
        while (true)
        {
            // Encender
            effector.enabled = true;
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(tiempoEncendido);

            // Apagar
            effector.enabled = false;
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(tiempoApagado);
        }
    }
}