using UnityEngine;

[CreateAssetMenu(fileName = "PowerupIce", menuName = "Powerups/Ice")]
public class PowerupIce : PowerupData
{
    private const short jumpAmount = 2;
    private const float extraJumpsHeight = 0.7f; //Jump height multiplier for extra jumps
    public override void ApplyEffect(GameObject pl)
    {
        if (pl.TryGetComponent<PlayerMovement>(out var player))
        {
            player.EnableMultipleJumps(jumpAmount, extraJumpsHeight);
            var powerup = player.GetComponent<PlayerOutfit>();
            powerup.ApplyOutfit(outfit);
        }
    }

    public override void RemoveEffect(GameObject pl)
    {
        if (pl.TryGetComponent<PlayerMovement>(out var player))
        {
            player.DisableMultipleJumps();
            var powerup = player.GetComponent<PlayerOutfit>();
            powerup.ResetOutfit();
        }
    }
}