using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Containers")]
    [SerializeField] private RectTransform mainUIContainer;
    [SerializeField] private RectTransform overlayUIContainer;
    [SerializeField] private TextMeshProUGUI txtScore;
    
    [Header("Collectables UI")]
    [SerializeField] private Image[] emeraldIcons;
    [SerializeField] private Sprite emeraldOutlineSprite;
    [SerializeField] private Sprite emeraldFilledSprite;
    
    private Camera mainCamera;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            GetComponent<Canvas>().worldCamera = mainCamera;
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

    public void SetCamera(Camera camera)
    {
        mainCamera = camera;
        GetComponent<Canvas>().worldCamera = mainCamera;
    }

    public void UpdateEmeraldIcons(int collected)
    {
        for (int i = 0; i < emeraldIcons.Length; i++)
        {
            emeraldIcons[i].sprite = (i < collected)
                ? emeraldFilledSprite
                : emeraldOutlineSprite;
        }
    }
}