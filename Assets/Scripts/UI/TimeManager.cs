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

        timeH = StartingTimeH;
        timeMin = StartingTimeMin;

        Events.OnToggleSleep += SetSleeping;
    }

    private void OnDestroy()
    {
        Events.OnToggleSleep -= SetSleeping;
    }

    private void Start()
    {
        UpdateClock();
    }

    private void Update()
    {
        second -= Time.deltaTime;

        if(second <= 0)
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
    }

    private void AddHour()
    {
        timeMin = 0;
        if (timeH < 23) { timeH++; }
        else { timeH = 0; }

        // Notify listeners that an hour has passed.
        if (!working && !sleeping)
        {
            Events.HourPassed();
        }

        // Notify listeners that 
        if (timeH == DayEndTimeH)
        {
            Events.ToggleSleep(true);
        }
        else if (timeH == StartingTimeH)
        {
            Events.ToggleSleep(false);
        }
        else if (timeH == WorkStartH)
        {
            working = true;
            Events.ToggleWork(working);
        }
        else if (timeH == WorkEndH)
        {
            working = false;
            Events.ToggleWork(working);
        }
    }

    private void ResetDay()
    {
        dayCount++;
        DayText.text = "Day " + dayCount;
        if(dayCount == GameManager.Instance.WinDay)
        {
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
            ResetDay();
        }
    }
}
