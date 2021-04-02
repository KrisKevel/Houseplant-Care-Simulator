using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SleepTime : MonoBehaviour
{
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
            GameManager.Instance.UpdateStress(Random.Range(GameManager.Instance.MinStressFromSleep, GameManager.Instance.MaxStressFromSleep));
        }
        gameObject.SetActive(sleeping);
    }

    public void StartNewDay()
    {
        Events.ToggleSleep(false);
    }
}
