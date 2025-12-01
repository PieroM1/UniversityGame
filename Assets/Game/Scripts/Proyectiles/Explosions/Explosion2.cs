using UnityEngine;
using System.Collections;

public class Explosion2 : MonoBehaviour
{
    [SerializeField] private float radius = 3;
    [SerializeField] private float power = 800;
    [SerializeField] private float delaySeconds = 4f;
    private Animator animator;
    private Collider2D col;

    private bool exploded = false;
    private Coroutine autoExplosionCoroutine;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        autoExplosionCoroutine = StartCoroutine(AutoExplosion());
    }

    private IEnumerator AutoExplosion()
    {
        yield return new WaitForSeconds(delaySeconds - 0.5f);
        TriggerExplosionAnimation();

        yield return new WaitForSeconds(0.5f);
        FinishExplosion();
    }

    public void ExplodeTouch()
    {
        if (exploded) return;

        if (autoExplosionCoroutine != null)
            StopCoroutine(autoExplosionCoroutine);

        StartCoroutine(ManualExplosion());
    }

    private IEnumerator ManualExplosion()
    {
        TriggerExplosionAnimation();
        yield return new WaitForSeconds(0.5f);
        FinishExplosion();
    }

    private void TriggerExplosionAnimation()
    {
        if (exploded) return;
        AudioManager.Instance.PlaySFX(SFXConstants.EXPLOSION);
        exploded = true;
        col.enabled = false;
        animator.SetBool("Exploded", true);
    }

    private void FinishExplosion()
    {
        Destroy(gameObject);
    }
}
