using UnityEngine;
using System.Collections;
public class WindManager : MonoBehaviour
{
    [SerializeField] private string tagViento = "Wind";
    [SerializeField] private float tiempoEncendido = 2f;
    [SerializeField] private float tiempoApagado = 5f;

    private float temporizador;
    private bool activo = true;
    
    private void Start()
    {
        temporizador = tiempoEncendido;
        SetEstado(true);
    }

    private void Update()
    {
        temporizador -= Time.deltaTime;

        if (temporizador <= 0f)
        {
            activo = !activo; // cambia estado
            SetEstado(activo);

            // reinicia temporizador según el estado
            temporizador = activo ? tiempoEncendido : tiempoApagado;
        }
    }

    private void SetEstado(bool estado)
    {
        foreach (GameObject viento in GameObject.FindGameObjectsWithTag(tagViento))
            viento.SetActive(estado);
    }
}
