using UnityEngine;
using UnityEngine.InputSystem;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;   // El objeto que se va a instanciar
    [SerializeField] private Transform spawnPoint;


    private void Update()
    {
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

}
