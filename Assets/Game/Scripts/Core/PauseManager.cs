using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject uiGame;

    private bool isPaused = false;


    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        uiGame.SetActive(false);
        isPaused = true;
        Debug.Log("Juego Pausado");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        uiGame.SetActive(true);
        isPaused = false;
        Debug.Log("Juego Reanudado");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void QuitLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        pauseMenu.SetActive(false);
    }
}
