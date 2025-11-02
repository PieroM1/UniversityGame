using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float waitTime = 1f;
    private bool isMoving = true;
    private static WaitForSeconds _waitForSeconds;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = pointB.position;
        _waitForSeconds = new WaitForSeconds(waitTime);
    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if (!isMoving) return;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            StartCoroutine(WaitAtPoint());
            targetPosition = (targetPosition == pointA.position)
                ? pointB.position
                : pointA.position;
        }
    }

    IEnumerator WaitAtPoint()
    {
        isMoving = false;
        animator.SetBool("isMoving", false);
        yield return _waitForSeconds;
        animator.SetBool("isMoving", true);
        isMoving = true;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }

        if (other.gameObject.CompareTag("Bomb"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
        if (other.gameObject.CompareTag("Bomb"))
        {
            other.transform.SetParent(null);
        }
    }
}