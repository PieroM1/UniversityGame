using UnityEngine;

public class Coll_PowerUp : Collectable
{  
    protected override void Collect(GameObject player)
    {
        AudioManager.Instance.PlaySFX(SFXConstants.POWER_UP);
        base.Collect(player);
    }
}
