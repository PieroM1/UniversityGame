using System;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject virtualCamara;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCamara.SetActive(true);
            Debug.Log("primera funcion");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCamara.SetActive(false);
            Debug.Log("segunda funcion");
        }
    }
}
