using System.Collections;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private Transform spawn;

    private bool playerInside = false;    


    void Start()
    {
        
    }

    IEnumerator Shoot()
    {
        while (playerInside)
        {
            Instantiate(projectilePrefab, spawn.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenShoots);
            Debug.Log("Disparo");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Dentro");
            StartCoroutine(Shoot());
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log("Fuera");
            StopCoroutine(Shoot());
        }
    }
}
