using UnityEngine;

public class PlayerPowerup : MonoBehaviour
{

    private PowerupData activePowerUp;

    public void ApplyPowerUp(GameObject player, PowerupData powerUp)
    {
        if (activePowerUp != null)
            activePowerUp.RemoveEffect(player);

        activePowerUp = powerUp;
        powerUp.ApplyEffect(player);
    }

    public void RemovePowerup(GameObject player)
    {
        activePowerUp?.RemoveEffect(player);
        activePowerUp = null;
    }
}