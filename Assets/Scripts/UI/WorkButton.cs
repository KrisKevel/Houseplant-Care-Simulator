using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkButton : MonoBehaviour
{
    void Awake()
    {
        Events.OnToggleWork += DisableButton;
        Events.OnToggleSleep += ShowButton;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnToggleWork -= DisableButton;
        Events.OnToggleSleep -= ShowButton;
    }

    public void GoWork()
    {
        Events.ToggleWork(true);
    }

    void DisableButton(bool working)
    {
        if(working)
        {
            gameObject.SetActive(false);
        }
    }

    void ShowButton(bool sleeping)
    {
        if (!sleeping)
        {
            gameObject.SetActive(true);
        }
    }
}
