using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepButton : MonoBehaviour
{
    void Awake()
    {
        Events.OnToggleSleep += DisableButton;
        Events.OnToggleWork += ShowButton;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnToggleSleep -= DisableButton;
        Events.OnToggleWork -= ShowButton;
    }

    public void GoSleep()
    {
        Events.ToggleSleep(true);
    }

    void DisableButton(bool evening)
    {
        if(!evening)
        {
            gameObject.SetActive(false);
        }
    }

    void ShowButton(bool working)
    {
        if (!working)
        {
            gameObject.SetActive(true);
        }
    }
}
