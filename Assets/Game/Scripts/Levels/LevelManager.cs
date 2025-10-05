using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public AudioConstants backgroundMusic;
    public int totalCollectables = 3;
    private int collectedItems = 0;

    private void Start()
    {
        AudioManager.Instance.ChangeBackgroundMusic(backgroundMusic);
        collectedItems = 0;
        UIManager.Instance.ShowHUD(true);
    }

    public void CollectItem(int value)
    {
        collectedItems += value;
        GameManager.Instance.UpdateScore(value);

        if (collectedItems >= totalCollectables)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        Debug.Log("Nivel completado");
    }
}
