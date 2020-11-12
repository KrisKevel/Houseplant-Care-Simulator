using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI TimeText;
    public int StartingTimeH;
    public int StartingTimeMin;
    public int DayEndTimeH;

    private int timeH;
    private int timeMin;
    private float second = 1.0f;
    private int dayCount;

    // Start is called before the first frame update
    void Awake()
    {
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
            //Day end event will be called here
            ResetDay();
        }
    }

    private void ResetDay()
    {
        timeH = StartingTimeH;
        timeMin = StartingTimeMin;
        dayCount++;
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
}
