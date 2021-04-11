using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SleepTime : MonoBehaviour
{
    public StressNotification Notification;

    private int _multiplierModifier = 2;

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
            float multiplier = Random.Range(0, _multiplierModifier) == 0 ? 1 : -1;
            float morningStress = multiplier * stress;
            GameManager.Instance.UpdateStress(morningStress);
            
            if (stressBeforeMorning == 100)
            {
                if (morningStress < 0)
                {
                    Notification.ShowNotification();
                    _multiplierModifier--;
                }
                else
                {
                    GameManager.Instance.EndGame();
                    Events.GameOver();
                }
            }
        }
        gameObject.SetActive(sleeping);
    }

    public void StartNewDay()
    {
        Events.ToggleSleep(false);
    }
}
