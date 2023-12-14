using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLightController : MonoBehaviour
{
    private void Awake()
    {
        TimeManager.onTimeAdded += CheckTime;
    }


    private void OnDestroy()
    {
        TimeManager.onTimeAdded -= CheckTime;
    }


    private void CheckTime(float currentTime)
    {
        if (this.gameObject.name == "WorldLight")
        {
            if (currentTime >= 19f || currentTime < 3f)
            {
                this.gameObject.GetComponent<Light>().intensity = 0.0f;
            }
            else if (currentTime >= 3f && currentTime < 7f)
            {
                this.gameObject.GetComponent<Light>().intensity = 0.1f;
            }
            else if (currentTime >= 7f && currentTime <= 10f)
            {
                this.gameObject.GetComponent<Light>().intensity = 0.3f;
            }
            else if (currentTime >= 11f && currentTime <= 14f)
            {
                this.gameObject.GetComponent<Light>().intensity = 0.5f;
            }
            else if (currentTime >= 15f && currentTime <= 18f)
            {
                this.gameObject.GetComponent<Light>().intensity = 0.2f;
            }
        }
        else
        {
            if (currentTime >= 18f && currentTime <= 20f)
            {
                this.gameObject.GetComponent<Light>().intensity = 0.4f;
            }
            else if (currentTime >= 21f || currentTime <= 4f)
            {
                this.gameObject.GetComponent<Light>().intensity = 1f;
            }
            else
            {
                this.gameObject.GetComponent<Light>().intensity = 0.0f;
            }
        }

    }
}
