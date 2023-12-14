using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameStateData
{
    //Player Data
    public string playerName;
    public float playerCashMoney = 5000f;
    public float playerBankSavingsMoney = 0f;
    

    //Game World Data
    public float time = 7f;
    public float day = 1f;
    public int gameLevel = 1;


    //Meetup system
    public Building meetupBuilding;
    public float meetupTime;
    public int meetupDay;
    public CharactersScriptableObj meetupCharacter;
    public bool pendingMeetup = false;
}
