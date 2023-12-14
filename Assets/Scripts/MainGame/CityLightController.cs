using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLightController : MonoBehaviour
{
    private void Awake()
    {
        TimeManager.onDayAdded += CheckTime;
    }


    private void CheckTime(int day)
    {
        
    }
}
