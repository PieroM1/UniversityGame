using UnityEngine;
[CreateAssetMenu(fileName = "PowerupCut", menuName = "Powerups/Cut")]
public class PowerupCut : PowerupData
{
    private short damage = 0;
    private float cutCooldown = 1f;
    private float attackDuration = 0.2f;

    public override void ApplyEffect(GameObject pl)
    {
        if (pl.TryGetComponent<PlayerMovement>(out var player))
        {
            player.EnableAttackAbility();
            var powerup = player.GetComponent<PlayerOutfit>();
            powerup.ApplyOutfit(outfit);
        }
    }

    public override void RemoveEffect(GameObject pl)
    {
        if (pl.TryGetComponent<PlayerMovement>(out var player))
        {
            player.DisableAttackAbility();
            var powerup = player.GetComponent<PlayerOutfit>();
            powerup.ResetOutfit();
        }
    }
}