﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float StartingFunds = 100f;
    public float DailyPay = 50f;
    public int WinDay = 8;
    public float AOE = 1f;
    public float MinStressFromWork;
    public float MaxStressFromWork;
    public float MinStressFromSleep; 
    public float MaxStressFromSleep;
    public float InitialStress;

    private void Awake()
    {
        Instance = this;
        PlayerPrefs.SetFloat("Funds", StartingFunds);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Events.OpenMenu();
        }
    }

    public float GetFunds()
    {
        return PlayerPrefs.GetFloat("Funds");
    }

    public void UpdateFunds(float amount)
    {
        PlayerPrefs.SetFloat("Funds", GetFunds() + amount);
    }

    public void AddDailySalary()
    {
        UpdateFunds(DailyPay);
    }
}
