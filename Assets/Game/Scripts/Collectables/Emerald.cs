using UnityEngine;

[CreateAssetMenu(fileName = "New Emerald", menuName = "Collectables/Emerald")]
public class Emerald : CollectableData
{
    public override void Collect()
    {
        LevelManager levelManager = FindAnyObjectByType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.CollectEmerald();
        }
    }
}
