using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] private Button startButton;
    [SerializeField] private Button level1Button; //temporary
    [SerializeField] private Button level2Button; //temporary
    [SerializeField] private Button level3Button; //temporary
    [SerializeField] private Button level4Button; //temporary
    public void Awake()
    {
        AudioManager.Instance.ChangeBackgroundMusic(AudioConstants.MENU);
    }

    private void Start()
    {
        //startButton.onClick.AddListener(StartGame);
        level1Button.onClick.AddListener(GoToLevel1);
        level2Button.onClick.AddListener(GoToLevel2);
        level3Button.onClick.AddListener(GoToLevel3);
        level4Button.onClick.AddListener(GoToLevel4);
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

    public void GoToLevel3()
    {
        AudioManager.Instance.StopBackgroundMusic();
        SceneManager.LoadScene("1-3");
    }

    public void GoToLevel4()
    {
        AudioManager.Instance.StopBackgroundMusic();
        SceneManager.LoadScene("1-4");
    }
}   