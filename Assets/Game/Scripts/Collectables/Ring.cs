using UnityEngine;

[CreateAssetMenu(fileName = "New Ring", menuName = "Collectables/Ring")]
public class Ring : CollectableData
{
    public int scoreValue;
    public override void Collect()
    {
        GameManager.Instance.UpdateScore(scoreValue);
    }
}
