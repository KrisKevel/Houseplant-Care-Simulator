using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI DayText;
    public int StartingTimeH;
    public int StartingTimeMin;
    public int WorkStartH;
    public int WorkEndH;
    public int DayEndTimeH;

    public Notification WorkNotification;

    private int timeH;
    private int timeMin;
    private float second = 1.0f;
    private int dayCount = 1;
    private bool working = false;
    private bool sleeping = false;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        SetTime(StartingTimeH, StartingTimeMin);

        Events.OnToggleSleep += SetSleeping;
        Events.OnToggleWork += SetWorking;
    }

    private void OnDestroy()
    {
        Events.OnToggleSleep -= SetSleeping;
        Events.OnToggleWork -= SetWorking;
    }

    private void Start()
    {
        UpdateClock();
    }

    private void Update()
    {
        if (GameManager.Instance.GameIsGoing)
        {
            second -= Time.deltaTime;

            if (second <= 0)
            {
                if (working || sleeping)
                {
                    AddHour();
                }
                else
                {
                    AddMinute();
                }
                UpdateClock();
                second = 1.0f;
            }
        }
    }

    private void AddMinute()
    {
        if (timeMin < 59)
        {
            timeMin++;
        }
        else
        {
            AddHour();
        }

        if (timeMin == 50 && timeH == WorkStartH - 1)
        {
            WorkNotification.ShowNotification();
        }
    }

    private void AddHour()
    {
        timeMin = 0;
        if (timeH < 23) { timeH++; }
        else { timeH = 0; }

        // Notify listeners that an hour has passed.
        Events.HourPassed();

        // Notify listeners that 
        if (timeH == DayEndTimeH && !sleeping)
        {
            Events.ToggleSleep(true);
        }
        else if (timeH == StartingTimeH)
        {
            Events.ToggleSleep(false);
        }
        else if (timeH == WorkEndH)
        {
            Events.ToggleWork(false);
        }
    }

    private void ResetDay()
    {
        dayCount++;
        DayText.text = "Day " + dayCount;
        SetTime(StartingTimeH, StartingTimeMin);
        UpdateClock();
        if (dayCount == GameManager.Instance.WinDay)
        {
            GameManager.Instance.EndGame();
            Events.Win();
        }
    }

    private void UpdateClock()
    {
        TimeText.text = GetCurrentTime();
    }

    public void SetTime(int h, int min)
    {
        timeH = h;
        timeMin = min;
    }

    public int GetCurrentTimeHours()
    {
        return timeH;
    }

    public string GetCurrentTime()
    {
        string timeHString;
        string timeMinString;

        if (timeH < 10)
        {
            timeHString = "0" + timeH;
        }
        else
        {
            timeHString = timeH.ToString();
        }

        if (timeMin < 10)
        {
            timeMinString = "0" + timeMin;
        }
        else
        {
            timeMinString = timeMin.ToString();
        }

        return timeHString + ":" + timeMinString;
    }

    void SetSleeping(bool sleep)
    {
        sleeping = sleep;

        if(!sleeping)
        {
            GameManager.Instance.UnpauseGame();
            if (GameManager.Instance.CurrentState != GameManager.GameState.tutorial)
            {
                ResetDay();
            }
        }
        else
        {
            GameManager.Instance.PauseGame();
        }
    }

    void SetWorking(bool work)
    {
        working = work;

        if (working)
        {
            CheckIfInTimeForWork();
        }
    }

    void CheckIfInTimeForWork()
    {
        // Late for work
        if (timeH == WorkStartH && timeMin > 0 || timeH > WorkStartH)
        {
            float lateMin = (timeH - WorkStartH) * 60 + timeMin;
            float workDayLenghtMin = (WorkEndH - WorkStartH) * 60;
            float amountToDeduct = (lateMin / workDayLenghtMin) * GameManager.Instance.DailyPay;
            GameManager.Instance.UpdateFunds(-amountToDeduct);
        }
    }
}
