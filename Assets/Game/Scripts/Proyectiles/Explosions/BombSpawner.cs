using UnityEngine;
using UnityEngine.InputSystem;

public class BombSpawner : MonoBehaviour
{

    //SpawnBomb
    [SerializeField] private GameObject prefab;   // El objeto que se va a instanciar
    [SerializeField] private Transform spawnPoint;

    //CheckGround
    public bool ground;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform spawnCheck;
    [SerializeField] private Vector3 boxDimensions;

    //CheckJump
    private PlayerMovement playerMovement;

    private GameObject currentBomb; 

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        bool canSpawn = currentBomb == null;
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame && playerMovement.grounded == true && ground == false && canSpawn)
        {
            currentBomb = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    private void FixedUpdate()
    {
        ground = Physics2D.OverlapBox(spawnCheck.position, boxDimensions, 0f, groundLayer);
        if (ground)
        {
            Debug.Log("Bomba choca con pared");
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnCheck.position, boxDimensions);
    }
}
