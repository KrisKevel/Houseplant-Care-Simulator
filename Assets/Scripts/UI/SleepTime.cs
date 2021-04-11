using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SleepTime : MonoBehaviour
{
    public StressNotification Notification;

    private void Awake()
    {
        Events.OnToggleSleep += TogglePanel;
    }

    private void OnDestroy()
    {
        Events.OnToggleSleep -= TogglePanel;
    }

    private void Start()
    {
        TogglePanel(false);
    }

    private void TogglePanel(bool sleeping)
    {
        if (!sleeping)
        {
            float stressBeforeMorning = GameManager.Instance.GetStress();
            float stress = Random.Range(GameManager.Instance.MinStressFromSleep, GameManager.Instance.MaxStressFromSleep);
            float multiplier = Random.Range(0, 3) == 1 ? 1 : -1;
            GameManager.Instance.UpdateStress(multiplier*stress);
            
            if (stressBeforeMorning == 100 && multiplier < 0)
            {
                Notification.ShowNotification();
            }
        }
        gameObject.SetActive(sleeping);
    }

    public void StartNewDay()
    {
        Events.ToggleSleep(false);
    }
}
