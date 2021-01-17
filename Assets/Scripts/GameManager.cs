using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float StartingFunds = 100f;
    public float DailyPay = 50f;

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
