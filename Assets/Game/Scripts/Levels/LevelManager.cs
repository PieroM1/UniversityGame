using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public AudioConstants backgroundMusic;
    private int totalEmeralds = 3;
    private int collectedEmeralds = 0;
    [SerializeField] private int emeraldValue = 100;

    private void Start()
    {
        AudioManager.Instance.ChangeBackgroundMusic(backgroundMusic);
        collectedEmeralds = 0;
        UIManager.Instance.ShowHUD(true);
        var camera = FindFirstObjectByType<Camera>();
        UIManager.Instance.SetCamera(camera);
    }

    public void CollectEmerald()
    {
        collectedEmeralds++;
        GameManager.Instance.UpdateScore(emeraldValue);
        UIManager.Instance.UpdateEmeraldIcons(collectedEmeralds);
    }

    public void LevelComplete()
    {
        UIManager.Instance.ShowHUD(false);
        SceneManager.LoadScene("MainMenu");
    }
}
