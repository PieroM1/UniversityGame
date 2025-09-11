using UnityEngine;
using UnityEngine.InputSystem;

public class BombSpawner : MonoBehaviour
{
   [SerializeField] private GameObject bombPrefab;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mouse = Mouse.current.position.ReadValue();
            Vector3 world = cam.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, -cam.transform.position.z));
            world.z = 0f;

            Instantiate(bombPrefab, world, Quaternion.identity);
        }
    }
}
