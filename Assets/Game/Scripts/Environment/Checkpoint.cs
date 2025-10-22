using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isActive = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            ActivateCheckpoint(other.transform);
        }
    }

    private void ActivateCheckpoint(Transform player)
    {
        GameManager.Instance.UpdateCheckpoint(transform.position);
        AudioManager.Instance.PlaySFX(SFXConstants.CHECKPOINT);
        ChangeActiveState(true);

        // Deactivate other checkpoints
        foreach (Checkpoint checkPoint in FindObjectsByType<Checkpoint>(FindObjectsSortMode.None))
        {
            if (checkPoint != this) checkPoint.ChangeActiveState(false);
        }

        Debug.Log("Checkpoint activated in" + transform.position);
    }

    private void ChangeActiveState(bool state)
    {
        isActive = state;
        animator.SetBool("isActive", state);
    }
}
