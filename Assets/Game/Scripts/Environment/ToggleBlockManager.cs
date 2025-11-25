using System;
using UnityEngine;

public class ToggleBlockManager : MonoBehaviour
{
    public static Action<bool> OnToggleBlocks;

    private static bool currentState = true;

    public static void Toggle()
    {
        currentState = !currentState;
        OnToggleBlocks?.Invoke(currentState);
    }
}
