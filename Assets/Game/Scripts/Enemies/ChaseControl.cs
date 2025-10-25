using UnityEngine;

public class ChaseControl : MonoBehaviour
{

    public FlyingEnemy[] enemyArray;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (FlyingEnemy enemy in enemyArray)
            {
                enemy.chase = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(FlyingEnemy enemy in enemyArray)
            {
                enemy.chase = false;
            }
        }
    }
}
