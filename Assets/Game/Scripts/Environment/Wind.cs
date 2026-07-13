using UnityEngine;
using System.Collections;
public class Wind : MonoBehaviour
{
    private AreaEffector2D effector;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float timeOn = 1f;
    [SerializeField] private float timeOff = 3f;

    void Start()
    {
        effector = GetComponent<AreaEffector2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(timeOn != -1f) StartCoroutine(Cycle()); //If timeOn is -1, wind is always on
    }

    IEnumerator Cycle()
    {
        while (true)
        {
            // Enable
            EnableWind();
            yield return new WaitForSeconds(timeOn);

            // Disable
            DisableWind();
            yield return new WaitForSeconds(timeOff);
        }
    }

    public void EnableWind()
    {
        effector.enabled = true;
        spriteRenderer.enabled = true;
    }
    public void DisableWind()
    {
        effector.enabled = false;
        spriteRenderer.enabled = false;
    }
}