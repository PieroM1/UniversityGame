using UnityEngine;

[CreateAssetMenu(menuName = "Player/Outfit Data")]
public class Outfit : ScriptableObject
{
    //These are default animation clips of the player
    public AnimationClip idleClip;
    public AnimationClip runClip;
    public AnimationClip jumpClip;
    public AnimationClip fallClip;
    //This is a special animation clip for a unique powerup effect
    public AnimationClip specialClip;
}