using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Containers")]
    [SerializeField] private RectTransform mainUIContainer;
    [SerializeField] private RectTransform overlayUIContainer;
    [SerializeField] private TextMeshProUGUI txtScore;

    private void Awake()
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

    public void ShowHUD(bool show)
    {
        mainUIContainer.gameObject.SetActive(show);
    }

    public void ShowOverlay(GameObject overlayPrefab)
    {
        Instantiate(overlayPrefab, overlayUIContainer);
    }

    public void UpdateScoreUI(int score)
    {
        txtScore.text = score.ToString();
    }
}