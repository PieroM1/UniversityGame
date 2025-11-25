using UnityEngine;

public class Switch : MonoBehaviour
{

    public static event System.Action<bool> OnToggle;

    private static bool primaryActive = true;

    public static void Toggle()
    {
        primaryActive = !primaryActive;
        OnToggle?.Invoke(primaryActive);
        ToggleBlockManager.Toggle();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Toggle();
        }
    }
}