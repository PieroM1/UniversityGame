using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public AudioConstants backgroundMusic;
    public int totalEmeralds = 3;
    private int collectedEmeralds = 0;
    [SerializeField] private int emeraldValue = 100;

    private void Start()
    {

    }

    public void CollectItem()
    {
        collectedEmeralds++;
        GameManager.Instance.UpdateScore(emeraldValue);

        if (collectedEmeralds >= totalEmeralds)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        Debug.Log("Nivel completado");
    }
}
