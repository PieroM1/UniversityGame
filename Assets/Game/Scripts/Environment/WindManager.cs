using UnityEngine;
using System.Collections;
public class WindManager : MonoBehaviour
{
    [Header("Wind Object")]
    [SerializeField] private GameObject windObject;

    [Header("Timing Settings")]
    [SerializeField] private float timeTurnedOn = 2f;
    [SerializeField] private float timeTurnedOff = 5f;

    private float timer;
    private bool isActive = true;
    private bool isDisabled = false;

    private void Start()
    {
        if (windObject == null)
            windObject = gameObject;

        timer = timeTurnedOn;
        SetState(true);
    }

    private void Update()
    {
        if (isDisabled) return;
        
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            ToogleState();
        }
    }

    private void SetState(bool state)
    {
        windObject.SetActive(state);
    }

    private void ToogleState()
    {
        isActive = !isActive;
        windObject.SetActive(isActive);
        timer = isActive ? timeTurnedOn : timeTurnedOff;
    }

    public void DisableWind()
    {
        isDisabled = true;
        SetState(false);
        timer = float.MaxValue; // Prevent further toggling
    }

    public void EnableWind()
    {
        if (isDisabled)
        {
            isDisabled = false;
            isActive = true;
            SetState(true);
            timer = timeTurnedOn;
        }
    }
}