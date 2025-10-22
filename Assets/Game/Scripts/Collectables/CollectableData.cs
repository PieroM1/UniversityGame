using UnityEngine;

public abstract class CollectableData : ScriptableObject
{
    public SFXConstants soundEffect;
    public virtual void Collect() {}
    public virtual void ApplyEffect(GameObject target){}
}
