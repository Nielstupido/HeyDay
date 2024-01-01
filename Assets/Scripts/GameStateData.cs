using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[Serializable]
public class GameStateData
{
    //Player Data
    public string playerName = "";
    public int playerAge = 19;
    public string playerGender;
    public int currentCharacter;
    public List<string> contactList = new List<string>();    
    public List<float> playerStatsDict = new List<float>() 
                                                            {
                                                                100f,
                                                                100f,
                                                                100f,
                                                                5000f,
                                                            };

    public float playerCash = 5000f;
    public float playerBankSavings = 0f;
    public float playerTotalDebt = 0f;
    public float playerHospitalOutstandingDebt = 0f;
    public bool isPlayerHasBankAcc = false;

    public float playerLvlBillExpenses = 0f;
    public float playerLvlSavings = 0f;
    public float playerLvlConsumablesExpenses = 0f;
    public float playerLvlEmergencyFunds = 0f;

    public string goalCourse = UniversityCourses.NONE.ToString();
    public string courseEnrolled = UniversityCourses.NONE.ToString();
    public string studyFieldEnrolled = StudyFields.NONE.ToString();
    public float playerStudyHours = 0f;
    public float playerEnrolledCourseDuration = 0f;

    public List<string> playerOwnedVehicles = new List<string>();
    public List<string> playerOwnedAppliances = new List<string>();
    public int groceryBarValue = 0;

    public string currentPlayerJobName;
    public string currentPlayerJobEstab;
    public Dictionary<string,float> playerWorkFieldHistory = new Dictionary<string, float>();
    public float currentWorkHours = 0f;
    public string currentPlayerPlace;

  

    //Time Manager
    public float currentTime = 7f; //time will start at 7AM
    public int currentDayCount = 1;



    //GameManager
    public List<int> charactersID = new List<int>();
    public List<string> charactersName = new List<string>();
    public List<string> charactersRelStatus = new List<string>();
    public List<int> charactersRelStatBarValue = new List<int>();
    public List<float> charactersCurrentDebt = new List<float>();
    public List<bool> charactersNumberObtained = new List<bool>();
    public List<bool> charactersBeenFriends = new List<bool>();
    public List<bool> charactersGotCalledToday = new List<bool>();

    public int currentGameLevel = 1;
    public int inflationDuration = 0;
    public float inflationRate = 0f;



    //ResBuilding
    public int stayCount = 0;
    public float unpaidBill = 0f;
    public int daysUnpaidRent = 0;



    //PlayerTravelManager
    public string currentVisitedBuilding;



    //LevelManager
    public List<string> currentActiveMissionsID = new List<string>();
    public List<string> currentActiveMissionsStatus = new List<string>();
    public List<float> currentActiveMissionsCurrentNumber = new List<float>();



    //HospitalManger
    public int daysUnpaid = 0; 



    //BudgetSystem
    public float currentBillsBudget = 0f;
    public float currentSavingsBudget = 0f;
    public float currentConsumablesBudget = 0f;
    public float currentEmergencyBudget = 0f;



    //Meetup system
    public string meetupBuilding;
    public float meetupTime = 0f;
    public int meetupDay = 0;
    public int meetupCharacter;
    public bool pendingMeetup = false;
}
