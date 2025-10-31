using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    public void Awake()
    {
        AudioManager.Instance.ChangeBackgroundMusic(AudioConstants.MENU);
    }

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        AudioManager.Instance.StopBackgroundMusic();
        SceneManager.LoadScene("PruebaPowerups");
    }
}   