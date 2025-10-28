using UnityEngine;

public abstract class CollectableData : ScriptableObject
{
    public SFXConstants soundEffect;
    public virtual void Collect() {}
}
