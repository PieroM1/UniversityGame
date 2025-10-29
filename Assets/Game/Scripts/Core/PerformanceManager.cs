using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    public static PerformanceManager Instance { get; private set; }
    private void Awake()
    {
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;
    }

    void Start()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}