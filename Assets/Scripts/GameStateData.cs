using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
public class GameStateData
{
    //Player Data
    public string playerName = null;
    public int playerAge = 19;
    public Gender playerGender;
    public CharactersScriptableObj currentCharacter;
    public List<string> contactList = new List<string>();    
    public Dictionary<PlayerStats, float> playerStatsDict = new Dictionary<PlayerStats, float>() 
                                                            {
                                                                {PlayerStats.HAPPINESS, 100f},
                                                                {PlayerStats.HUNGER, 100f},
                                                                {PlayerStats.ENERGY, 100f},
                                                                {PlayerStats.MONEY, 5000f},
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

    public UniversityCourses goalCourse = UniversityCourses.NONE;
    public UniversityCourses courseEnrolled = UniversityCourses.NONE;
    public StudyFields studyFieldEnrolled = StudyFields.NONE;
    public float playerStudyHours = 0f;
    public float playerEnrolledCourseDuration = 0f;

    public List<Items> playerOwnedVehicles = new List<Items>();
    public List<Items> playerOwnedAppliances = new List<Items>();
    public int groceryBarValue = 0;

    public JobPositions currentPlayerJob = null;
    public Dictionary<JobFields,float> playerWorkFieldHistory = new Dictionary<JobFields, float>();
    public float currentWorkHours = 0f;
    public ResBuilding currentPlayerPlace = null;

  

    //Time Manager
    public float currentTime = 7f; //time will start at 7AM
    public int currentDayCount = 1;



    //GameManager
    public List<CharactersScriptableObj> characters = new List<CharactersScriptableObj>();
    public int currentGameLevel = 1;
    public int inflationDuration = 0;
    public float inflationRate = 0f;



    //ResBuilding
    public int stayCount = 0;
    public float unpaidBill = 0f;
    public int daysUnpaidRent = 0;



    //PlayerTravelManager
    public Building currentVisitedBuilding = null;



    //LevelManager
    public List<MissionsScriptableObj> currentActiveMissions = new List<MissionsScriptableObj>();



    //HospitalManger
    public int daysUnpaid = 0; 



    //BudgetSystem
    public float currentBillsBudget = 0f;
    public float currentSavingsBudget = 0f;
    public float currentConsumablesBudget = 0f;
    public float currentEmergencyBudget = 0f;



    //Meetup system
    public Building meetupBuilding = null;
    public float meetupTime = 0f;
    public int meetupDay = 0;
    public CharactersScriptableObj meetupCharacter = null;
    public bool pendingMeetup = false;
}
