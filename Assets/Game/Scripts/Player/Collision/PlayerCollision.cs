using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            Respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        AudioManager.Instance.PlaySFX(SFXConstants.RESPAWN);
        transform.position = GameManager.Instance.lastCheckpointPosition;
    }
}
