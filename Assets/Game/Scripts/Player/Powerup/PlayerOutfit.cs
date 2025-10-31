using System.Collections.Generic;
using UnityEngine;

public class PlayerOutfit : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController baseController;
    [SerializeField] private Outfit defaultOutfit;
    [SerializeField] private List<Outfit> outfits;

    private Animator animator;
    private AnimatorOverrideController overrideController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        overrideController = new AnimatorOverrideController(baseController);
        animator.runtimeAnimatorController = overrideController;
        ApplyOutfit(defaultOutfit);
    }

    public void ApplyOutfit(Outfit outfit)
    {
        overrideController["Player_Idle_Default"] = outfit.idleClip;
        overrideController["Player_Run_Default"] = outfit.runClip;
        overrideController["Player_Jump_Default"] = outfit.jumpClip;
        overrideController["Player_Fall_Default"] = outfit.fallClip;
        if (outfit.extraJumpClip != null)
            overrideController["Player_ExtraJump_Default"] = outfit.extraJumpClip;
        if (outfit.attackClip != null)
            overrideController["Player_Attack_Default"] = outfit.attackClip;
        if (outfit.dashClip != null)
            overrideController["Player_Dash_Default"] = outfit.dashClip;
    }

    public void ResetOutfit()
    {
        ApplyOutfit(defaultOutfit);
    }
}