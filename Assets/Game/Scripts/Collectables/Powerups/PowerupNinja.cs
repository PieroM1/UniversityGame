using UnityEngine;
[CreateAssetMenu(fileName = "PowerupNinja", menuName = "Powerups/Ninja")]
public class PowerupNinja : PowerupData
{
    private float dashDistance = 3f;
    private float dashCooldown = 1f;

    public override void ApplyEffect(GameObject pl)
    {
        if (pl.TryGetComponent<PlayerMovement>(out var player))
        {
            player.EnableDashAbility(dashDistance, dashCooldown);
            var powerup = player.GetComponent<PlayerOutfit>();
            powerup.ApplyOutfit(outfit);
        }
    }

    public override void RemoveEffect(GameObject pl)
    {
        if (pl.TryGetComponent<PlayerMovement>(out var player))
        {
            player.DisableDashAbility();
            var powerup = player.GetComponent<PlayerOutfit>();
            powerup.ResetOutfit();
        }
    }
}