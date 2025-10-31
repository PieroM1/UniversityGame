using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] private Button startButton;
    [SerializeField] private Button level1Button; //temporary
    [SerializeField] private Button level2Button; //temporary
    public void Awake()
    {
        AudioManager.Instance.ChangeBackgroundMusic(AudioConstants.MENU);
    }

    private void Start()
    {
        //startButton.onClick.AddListener(StartGame);
        level1Button.onClick.AddListener(GoToLevel1);
        level2Button.onClick.AddListener(GoToLevel2);
    }

    public void StartGame()
    {
        AudioManager.Instance.StopBackgroundMusic();
        SceneManager.LoadScene("PruebaPowerups");
    }
    // Temporary
    public void GoToLevel1()
    {
        AudioManager.Instance.StopBackgroundMusic();
        SceneManager.LoadScene("1-1");
    }
    public void GoToLevel2()
    {
        AudioManager.Instance.StopBackgroundMusic();
        SceneManager.LoadScene("1-2");
    }
}   