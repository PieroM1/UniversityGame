using UnityEngine;

public class Coll_Coin : Collectable
{
    protected override void Collect(GameObject player)
    {
        GameManager.Instance.UpdateScore(10);
        AudioManager.Instance.PlaySFX(SFXConstants.RING);
        base.Collect(player);
    }
}
