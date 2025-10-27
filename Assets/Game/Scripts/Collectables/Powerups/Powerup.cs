using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private PowerupData powerup;
    private SpriteRenderer spriteRenderer;
    private Collider2D coll2D;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll2D = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            ApplyEffect(other);
    }

    private void ApplyEffect(Collider2D player)
    {
        if (powerup != null)
        {
            AudioManager.Instance.PlaySFX(SFXConstants.POWER_UP);
            if (player.TryGetComponent<PlayerPowerup>(out var playerPowerup))
            {
                playerPowerup.ApplyPowerUp(player.gameObject, powerup);
            }
        }

        if (coll2D != null) coll2D.enabled = false;
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        Destroy(gameObject);
    }
}