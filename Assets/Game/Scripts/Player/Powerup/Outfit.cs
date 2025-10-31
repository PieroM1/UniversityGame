using UnityEngine;

[CreateAssetMenu(menuName = "Player/Outfit Data")]
public class Outfit : ScriptableObject
{
    //These are default animation clips of the player
    public AnimationClip idleClip;
    public AnimationClip runClip;
    public AnimationClip jumpClip;
    public AnimationClip fallClip;
    public AnimationClip extraJumpClip;
    public AnimationClip attackClip;
    public AnimationClip dashClip;
}