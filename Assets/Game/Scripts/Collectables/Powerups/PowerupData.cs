using UnityEngine;

public class PowerupData : ScriptableObject
{
    [SerializeField] protected Outfit outfit;
    public virtual void ApplyEffect(GameObject pl)
    {
        if (pl.TryGetComponent<PlayerMovement>(out var player))
        {
            var powerup = player.GetComponent<PlayerOutfit>();
            powerup.ApplyOutfit(outfit);
        }
    }
    public virtual void RemoveEffect(GameObject pl)
    {
        if (pl.TryGetComponent<PlayerMovement>(out var player))
        {
            var powerup = player.GetComponent<PlayerOutfit>();
            powerup.ResetOutfit();
        }
    }
}