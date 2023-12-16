using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetUpSystem : MonoBehaviour
{
    private Building meetupBuilding;
    private float meetupTime;
    private int meetupDay;
    private CharactersScriptableObj meetupCharacter;
    private bool pendingMeetup = false;
    public static MeetUpSystem Instance {private set; get;}


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
    
        GameManager.onGameStarted += LoadGameData;
        GameManager.onGameStarted += SaveGameData;
    }


    private void LoadGameData()
    {
        this.meetupBuilding = GameManager.Instance.CurrentGameStateData.meetupBuilding;
        this.meetupTime = GameManager.Instance.CurrentGameStateData.meetupTime;
        this.meetupDay = GameManager.Instance.CurrentGameStateData.meetupDay;
        this.meetupCharacter = GameManager.Instance.CurrentGameStateData.meetupCharacter;
        this.pendingMeetup = GameManager.Instance.CurrentGameStateData.pendingMeetup;
    }


    private void SaveGameData()
    {
        GameManager.Instance.CurrentGameStateData.meetupBuilding = this.meetupBuilding;
        GameManager.Instance.CurrentGameStateData.meetupTime = this.meetupTime;
        GameManager.Instance.CurrentGameStateData.meetupDay = this.meetupDay;
        GameManager.Instance.CurrentGameStateData.meetupCharacter = this.meetupCharacter;
        GameManager.Instance.CurrentGameStateData.pendingMeetup = this.pendingMeetup;
    }


    private void Start()
    {
        TimeManager.onTimeAdded += CheckMeetupStatus;
        TimeManager.onDayAdded += CheckMeetupStatus;
    }


    private void OnDestroy()
    {
        GameManager.onGameStarted -= LoadGameData;
        GameManager.onSaveGameStateData -= SaveGameData;
        TimeManager.onTimeAdded -= CheckMeetupStatus;
        TimeManager.onDayAdded -= CheckMeetupStatus;   
    }

    private void OnEnable()
    {
        TimeManager.onTimeAdded += CheckMeetupStatus;
        TimeManager.onDayAdded += CheckMeetupStatus;
    }


    private void OnDisable()
    {
        TimeManager.onTimeAdded -= CheckMeetupStatus;
        TimeManager.onDayAdded -= CheckMeetupStatus;   
    }


    public void ResetMeetupDets()
    {
        meetupBuilding = null;
        meetupCharacter = null;
        meetupTime = 0f; 
        meetupDay = 0;
        pendingMeetup = false;
    }


    public bool CheckForPendingMeetup()
    {
        return pendingMeetup;
    }


    public bool CheckMeetupPlan(CharactersScriptableObj interactedCharacter)
    {
        if (interactedCharacter = meetupCharacter)
        {
            pendingMeetup = false;
            return true;
        }
        
        return false;
    }


    public string InviteNpcMeetup(string name)
    {
        string meetupPlan;
        CharactersScriptableObj currentCharacter = GameManager.Instance.GetCharacterValue(name);
        int randomNum = UnityEngine.Random.Range(1, 101);

        if (currentCharacter == null)
        {
            return null;
        }

        //reject
        if (randomNum == 1)
        {
            return "Sorry :(( I can't make it this time, but let's plan another meet-up soon. Bye!";
        }

        meetupPlan = CreateMeetupPlan(currentCharacter);
        if (meetupPlan == null)
        {
            return "Sorry :(( I can't make it this time, but let's plan another meet-up soon. Bye!";
        }

        //accept
        return "Sure! " + meetupPlan;
    }


    public ValueTuple<Building, CharactersScriptableObj, float, int> GetMeetupDets()
    {
        return (meetupBuilding, meetupCharacter, meetupTime, meetupDay);
    }


    private string CreateMeetupPlan(CharactersScriptableObj currentCharacter)
    {
        Building selectedBuilding;
        string meetupPlan = "";
        int targetMeetupDay;
        float randomTime;

        meetupPlan = "Let's meet later around ";
        targetMeetupDay = TimeManager.Instance.CurrentDayCount;
        randomTime = UnityEngine.Random.Range((int)TimeManager.Instance.CurrentTime + 1, 25);

        if (TimeManager.Instance.CurrentTime > 22f)
        {
            meetupPlan = "Let's meet tomorrow around ";
            targetMeetupDay += 1;
            randomTime = UnityEngine.Random.Range(1, 25);
        }

        meetupPlan += TimeManager.Instance.TransposeTimeValue((int)randomTime).ToString() + TimeManager.Instance.AmOrPm(randomTime) + " ";
        selectedBuilding = GameManager.Instance.GetOpenBuilding(randomTime);

        if (selectedBuilding == null)
        {
            return null;
        }
        
        meetupPlan += "at " + selectedBuilding.buildingStringName + ". " + "See you!";
        FinalizeMeetupDets(selectedBuilding, currentCharacter, randomTime, targetMeetupDay);

        return meetupPlan;
    }


    private void FinalizeMeetupDets(Building plannedBuilding, CharactersScriptableObj currentCharacter, float plannedTime, int plannedDay)
    {
        meetupBuilding = plannedBuilding;
        meetupCharacter = currentCharacter;
        meetupTime = plannedTime; 
        meetupDay = plannedDay;
        pendingMeetup = true;
    }


    private void CheckMeetupStatus(float time)
    {
        CheckMeetupStatus();
    }


    private void CheckMeetupStatus(int day)
    {
        CheckMeetupStatus();
    }


    private void CheckMeetupStatus()
    {
        if (!pendingMeetup)
        {
            return;
        }

        if (meetupDay != TimeManager.Instance.CurrentDayCount && meetupDay != (TimeManager.Instance.CurrentDayCount + 1))
        {
            return;
        }

        if ((meetupTime + 2f) >= TimeManager.Instance.CurrentTime)
        {
            GameManager.Instance.Characters[GameManager.Instance.Characters.IndexOf(meetupCharacter)].MeetupMissed();
            ResetMeetupDets();
        }
    }
}
