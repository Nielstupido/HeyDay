using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SleepManager : MonoBehaviour
{
    [SerializeField] private GameObject sleepingManagerOverlay;
    [SerializeField] private TextMeshProUGUI sleepHrsText;
    private float adtnlEnergyForSleep;
    private float sleepHrs;
    public static SleepManager Instance { get; private set; }


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


    public void IncrementSleepHrs()
    {
        sleepHrs = float.Parse(sleepHrsText.text);
        if (sleepHrs < 10f)
        {
            sleepHrsText.text = (++sleepHrs).ToString();
        }
        Debug.Log(sleepHrs);
    }

    
    public void DecrementSleepHrs()
    {
        sleepHrs = float.Parse(sleepHrsText.text);
        if (sleepHrs > 1f)
        {
            sleepHrsText.text = (--sleepHrs).ToString();
        }
    }


    public void ShowSleepOverlay(float adtnlPlayerEnergyForSleep)
    {
        sleepingManagerOverlay.SetActive(true);
        adtnlEnergyForSleep = adtnlPlayerEnergyForSleep;
    }


    public void TakeSleep()
    {
        sleepHrs = float.Parse(sleepHrsText.text);
        StartCoroutine(DoSleep(sleepHrs));
    }


    private IEnumerator DoSleep(float waitingTime)
    {
        AnimOverlayManager.Instance.StartAnim(ActionAnimations.RESIGN);
        TimeManager.Instance.AddClockTime(sleepHrs);
        Player.Instance.PlayerStatsDict[PlayerStats.ENERGY] += (sleepHrs * adtnlEnergyForSleep);
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ENERGY, Player.Instance.PlayerStatsDict);
        LevelManager.onFinishedPlayerAction(MissionType.SLEEPHR, sleepHrs);

        yield return new WaitForSeconds(waitingTime);

        sleepingManagerOverlay.SetActive(false);
        AnimOverlayManager.Instance.StopAnim();
        LifeEventsManager.Instance.StartLifeEvent(); //random (earthquake, inflation)

        yield return null;
    }
}
