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
        overrideController["JugadorQuieto"] = outfit.idleClip;
        overrideController["JugadorCorrer"] = outfit.runClip;
        overrideController["JugadorSalto"] = outfit.jumpClip;
        overrideController["JugadorCaida"] = outfit.fallClip;
    }

    public void ResetOutfit()
    {
        ApplyOutfit(defaultOutfit);
    }
}