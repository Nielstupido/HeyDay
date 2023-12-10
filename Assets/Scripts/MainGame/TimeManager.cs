using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clockValue;
    [SerializeField] private TextMeshProUGUI indicatorAMPM;
    [SerializeField] private TextMeshProUGUI clockValueSmallOverlay;
    [SerializeField] private TextMeshProUGUI indicatorAMPMSmallOverlay;
    [SerializeField] private TextMeshProUGUI dayValue;
    private float currentTime = 0f; //time will start at 7AM
    private int toggleCounter;
    private int currentDayCount = 1;
    public float CurrentTime { get{return currentTime;}}
    public int CurrentDayCount { get{return currentDayCount;}}
    public static TimeManager Instance { get; private set; }
    public delegate void OnDayAdded(int dayCount);
    public static OnDayAdded onDayAdded;
    public delegate void OnTimeAdded(float time);
    public static OnTimeAdded onTimeAdded;


    private void Awake() 
    { 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void AddClockTime(float addedClockValue)
    {
        currentTime += addedClockValue;
        int minutes = 0;
        int hours = 0;
        int transposedValue;

        hours = (int)currentTime;
        float temp = (currentTime % 1) * 100;
        minutes = (int)temp;

        if (currentTime % 1 != 0) //determines if value is whole number or not
        {
            if (minutes > 59)
            {
                minutes = 0;
                hours += 1;
                if (hours > 24)
                {
                    hours = 0;
                    toggleCounter++;
                }
                currentTime = hours;
                transposedValue = TransposeTimeValue((int)currentTime);
                clockValue.text = transposedValue.ToString() + ":00";
                clockValueSmallOverlay.text = transposedValue.ToString() + ":00";
            }
            else if (minutes < 10)
            {
                if (hours > 24)
                {
                    hours = 0;
                    toggleCounter++;
                }
                transposedValue = TransposeTimeValue((int)currentTime);
                clockValue.text = transposedValue.ToString() + ":0" + minutes.ToString();
                clockValueSmallOverlay.text = transposedValue.ToString() + ":0" + minutes.ToString();
            }
            else if (hours > 24)
            {
                hours = hours - 24;
                currentTime = hours + (minutes / 100);
                transposedValue = TransposeTimeValue((int)currentTime);
                clockValue.text = transposedValue.ToString() + ":" + minutes.ToString();
                clockValueSmallOverlay.text = transposedValue.ToString() + ":" + minutes.ToString();
                toggleCounter++;
            }
            else
            {
                transposedValue = TransposeTimeValue((int)currentTime);
                clockValue.text = transposedValue.ToString() + ":" + minutes.ToString();
                clockValueSmallOverlay.text = transposedValue.ToString() + ":" + minutes.ToString();
            }
        }
        else
        {
            if (hours > 24)
            {
                hours = hours - 24;
                currentTime = hours + (minutes / 100);
                transposedValue = TransposeTimeValue((int)currentTime);
                clockValue.text = transposedValue.ToString() + ":00";
                clockValueSmallOverlay.text = transposedValue.ToString() + ":00";
                toggleCounter++;
            }
            else
            {
                transposedValue = TransposeTimeValue((int)currentTime);
                clockValue.text = transposedValue.ToString() + ":00";
                clockValueSmallOverlay.text = transposedValue.ToString() + ":00";
            }
        }
        
        if (onTimeAdded != null)
        {
            onTimeAdded(currentTime);
        }
        
        AmOrPm();
        IncrementDayCount();
    }


    public int TransposeTimeValue(int currentTime)
    {
        switch (currentTime)
        {
            case 13: return 1;
            case 14: return 2;
            case 15: return 3;
            case 16: return 4;
            case 17: return 5;
            case 18: return 6;
            case 19: return 7;
            case 20: return 8;
            case 21: return 9;
            case 22: return 10;
            case 23: return 11;
            case 24: return 12;
            case 0: return 12;
            default: return (currentTime);
        }
    }


    public void AmOrPm()
    {
        if (currentTime >= 12)
        {
            indicatorAMPM.text = "PM";
            indicatorAMPMSmallOverlay.text = "PM";
        }
        else
        {
            indicatorAMPM.text = "AM";
            indicatorAMPMSmallOverlay.text = "AM";
        }
    }


    public string AmOrPm(float time)
    {
        if (time >= 12)
        {
            return "PM";
        }
        else
        {
            return "AM";
        }
    }


    public void IncrementDayCount()
    {
        if (toggleCounter == 1)
        {
            currentDayCount++;
            UpdateDayUI(currentDayCount);
            toggleCounter = 0;
            onDayAdded(currentDayCount);
        }
    }


    public void UpdateDayUI(int day)
    {
        dayValue.text = day.ToString();
    }
}
