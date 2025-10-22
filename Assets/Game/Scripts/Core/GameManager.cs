using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public int lives = 3;
    public Vector3 lastCheckpointPosition;

    private void Awake()
    {
        //Singleton pattern implementation
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        SceneManager.LoadScene("EscenaPrueba");
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        UIManager.Instance.UpdateScoreUI(score);
    }
    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
    }

    public void UpdateCheckpoint(Vector3 newCheckpoint)
    {
        lastCheckpointPosition = newCheckpoint;
    }
}
