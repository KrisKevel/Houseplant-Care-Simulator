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

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        timeH = StartingTimeH;
        timeMin = StartingTimeMin;
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
            AddMinute();
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
        if(timeH < DayEndTimeH)
        {
            timeH++;
        }
        else
        {
            ResetDay();
        }
        if(timeH == WorkStartH)
        {
            timeH = WorkEndH;
        }
    }

    private void ResetDay()
    {
        timeH = StartingTimeH;
        timeMin = StartingTimeMin;
        dayCount++;
        DayText.text = "Day " + dayCount;
    }

    private void UpdateClock()
    {
        string timeHString;
        string timeMinString;

        if(timeH < 10)
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

        TimeText.text = timeHString + ":" + timeMinString;
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
}
