using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private GameObject virtualCamara;

    private void Awake()
    {
        virtualCamara = transform.GetChild(0).gameObject;
    } 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCamara.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCamara.SetActive(false);
        }
    }
}
