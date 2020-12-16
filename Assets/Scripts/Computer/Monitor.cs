﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Monitor : MonoBehaviour
{
    void Awake()
    {
        Events.OnUseComputer += OpenPanel;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnUseComputer -= OpenPanel;
    }

    void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    public void CloseMonitor()
    {
        gameObject.SetActive(false);
    }
}
