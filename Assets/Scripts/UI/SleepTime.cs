﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SleepTime : MonoBehaviour
{
    private TextMeshProUGUI Time;

    private void Awake()
    {
        Events.OnToggleSleep += TogglePanel;
        Time = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void OnDestroy()
    {
        Events.OnToggleSleep -= TogglePanel;
    }

    private void Start()
    {
        TogglePanel(false);
    }

    private void Update()
    {
        Time.text = TimeManager.Instance.GetCurrentTime();
    }

    private void TogglePanel(bool sleeping)
    {
        gameObject.transform.parent.gameObject.SetActive(sleeping);
    }
}
