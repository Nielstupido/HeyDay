using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public enum TutorialState
    {
        BudgetSet,
        WelcomeScreen,
        MainGoals,
        ClickToContOverlay,
        ClickToContClosed,
        ToggleStats,
        PhoneUsed,
        MissionViewed,
        EnrollCourse,
        Study,
        OpenBankAcc,
        DepositMoney,
        Eat,
        ExitFoodplace,
        RentProperty,
        Sleep,
        ExitSuburbs,
        ApplyJob,
        Work,
        ExitWorkplace,
        Closing,
        Completed
    }

    [SerializeField] private GameObject tutorialPopUp;
    [SerializeField] private GameObject tutorialOverlay;
    [SerializeField] private GameObject welcomePopUp;
    [SerializeField] private GameObject budgetSetterOverlay;
    [SerializeField] private GameObject smallBotOverlay;
    [SerializeField] private GameObject phoneOverlay;
    [SerializeField] private GameObject missionOverlay;
    [SerializeField] private GameObject buildingInteriorOverlay;
    [SerializeField] private GameObject animationOverlay;
    [SerializeField] private GameObject promptOverlay;
    [SerializeField] private GameObject clickToContOverlay;
    [SerializeField] private Text tutorialText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject arrow;

    private TutorialState currentState = TutorialState.BudgetSet;
    private bool isBudgetSet = false;
    private bool tutorialStarted = false;
    private bool isStatsOverlayShown = false;
    private bool isPhoneUsed = false;
    private bool isMissionViewed = false;
    private bool isFoodEaten = false;
    private bool isSleepDone = false;
    private bool leftSuburbs = false;
    private bool isPlayerHired = false;
    private bool isSalaryReceived = false;
    private bool isExitedBuilding = false;
    private bool isDoneEating = false;


    public bool IsTutorialStarted { set{tutorialStarted = value;} get{return tutorialStarted;}}
    public bool IsStatsOverlayShown { set{isStatsOverlayShown = value;} get{return isStatsOverlayShown;}}
    public bool IsBudgetSet { set{isBudgetSet = value;} get{return isBudgetSet;}}
    public bool IsPhoneUsed { set{isPhoneUsed = value;} get{return isPhoneUsed;}}
    public bool IsMissionViewed { set{isMissionViewed = value;} get{return isMissionViewed;}}
    public bool IsFoodEaten { set{isFoodEaten = value;} get{return isFoodEaten;}}
    public bool IsSleepDone { set{isSleepDone = value;} get{return isSleepDone;}}
    public bool LeftSuburbs { set{leftSuburbs = value;} get{return leftSuburbs;}}
    public bool IsPlayerHired { set{isPlayerHired = value;} get{return isPlayerHired;}}
    public bool IsSalaryReceived { set{isSalaryReceived = value;} get{return isSalaryReceived;}}
    public bool IsExitedBuilding { set{isExitedBuilding = value;} get{return isExitedBuilding;}}
    public bool IsDoneEating { set{isDoneEating = value;} get{return isDoneEating;}}
    public static TutorialManager Instance {private set; get;}


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

    private void Start()
    {
        SetUIState(false, false);
        // StartTutorial();
    }

    private void Update()
    {
        if (!tutorialStarted)
        {
            return;
        }

        switch (currentState)
        {
            case TutorialState.BudgetSet:
                HandleBudgetSetState();
                break;
            case TutorialState.WelcomeScreen:
                HandleWelcomeScreenState();
                break;
            case TutorialState.MainGoals:
                HandleWelcomeMainGoalsScreen();
                break;
            case TutorialState.ClickToContOverlay:
                HandleClickToCont();
                break;
            case TutorialState.ClickToContClosed:
                HandleClickToContClosed();
                break;
            case TutorialState.ToggleStats:
                HandleStatsToggled();
                break;
            case TutorialState.PhoneUsed:
                HandlePhoneUsed();
                break;
            case TutorialState.MissionViewed:
                HandleMissionsViewed();
                break;
            case TutorialState.EnrollCourse:
                HandleCourseEnrolled();
                break;
            case TutorialState.Study:
                HandleStudyDone();
                break;
            case TutorialState.OpenBankAcc:
                HandleBankAccCreated();
                break;
            case TutorialState.DepositMoney:
                HandleSavingDone();
                break;
            case TutorialState.Eat:
                HandleFoodEaten();
                break;
            case TutorialState.ExitFoodplace:
                HandleDoneEating();
                break;
            case TutorialState.RentProperty:
                HandlePropertyRented();
                break;
            case TutorialState.Sleep:
                HandleSleepDone();
                break;
            case TutorialState.ExitSuburbs:
                HandleLeftSuburbs();
                break;
            case TutorialState.ApplyJob:
                HandleJobHired();
                break;
            case TutorialState.Work:
                HandleWorkDone();
                break;
            case TutorialState.ExitWorkplace:
                HandleExitedBuilding();
                break;
            case TutorialState.Closing:
                HandleClosing();
                break;
            case TutorialState.Completed:
                HandleTutorialDone();
                break;
        }
    }

    private void SetUIState(bool popupActive, bool overlayActive)
    {
        tutorialPopUp.SetActive(popupActive);
        tutorialOverlay.SetActive(overlayActive);
    }

    private void HandleBudgetSetState()
    {
        if (!IsBudgetSet && budgetSetterOverlay.activeSelf)
        {
            SetTutorialContent("Set your budget for this level. This can help you manage and keep track of your expenses throughout the game." +
                "\nTip: 50% on Expenses, 30% on Consumables, and 20% on Savings and Emergency Funds");
            DisplayTutorial();
        }
    }

    private void HandleWelcomeScreenState()
    {   
        if (IsBudgetSet && missionOverlay.activeSelf)
        {
            DisplayWelcomeScreen();
        }
    }

    private void HandleWelcomeMainGoalsScreen()
    {
        if (!missionOverlay.activeSelf)
        {
            SetTutorialContent("Your main goals are to graduate your assigned college degree " +
            "and save 100,000 pesos. To do that, you must manage your finances wisely, " +
            "take care of your health stats, and avoid bankruptcy through all levels.");
            DisplayTutorial();
            isMissionViewed = false;
        }
    }

    private void HandleClickToCont()
    {
        clickToContOverlay.SetActive(true);
        tutorialOverlay.SetActive(true);
    }

    private void HandleClickToContClosed()
    {
        SetTutorialContent("Your stats shown below can decrease and increase while playing. " +
            "\nTo toggle view your stats display, click then unclick a building.");
        DisplayTutorial();
    }

    private void HandleStatsToggled()
    {
        if (smallBotOverlay.activeSelf)
        {
            SetTutorialContent("View your life goals, track your finances, buy groceries, " +
            "or call a HeyDay Citizen through your phone. Try using it!");
            DisplayTutorial();
        }
    }
    private void HandlePhoneUsed()
    {
        if (IsPhoneUsed && !phoneOverlay.activeSelf)
        {
            SetTutorialContent("View your missions tracker to know what to do next. " +
            "Try viewing it now!");
            DisplayTutorial();
        }
    }

    private void HandleMissionsViewed()
    {
        if (IsMissionViewed && !missionOverlay.activeSelf)
        {
            SetTutorialContent("Great! Now let's start accomplishing each mission! " +
            "Go to the <b><color=#024563>UNIVERSITY</color></b> and enroll on your " +
            "<b><color=#024563>GOAL COURSE</color></b> \n" +
            "\n Tip: Your goal course can be viewed in the Goal Tracker App in your phone.");
            DisplayTutorial();
        }
    }

    private void HandleCourseEnrolled()
    {
        if (Player.Instance.PlayerEnrolledCourse == Player.Instance.GoalCourse)
        {
            SetTutorialContent("Congrats on getting officially enrolled! " +
            "Now, click the <b><color=#024563>STUDY</color></b> button and study for atleast 4 hours.");
            DisplayTutorial();
        }
    }

    private void HandleStudyDone()
    {
        if (Player.Instance.PlayerStudyHours >= 4 && !animationOverlay.activeSelf)
        {
            SetTutorialContent("Excellent! You've got a long way to go but you'll get there! " +
                "\n\nLet's now exit the building and go to the <b><color=#024563>BANK</color></b> " +
                "to <b><color=#024563>OPEN A SAVINGS ACCOUNT</color></b> to start saving money.");
            DisplayTutorial();
        }
    }

    private void HandleBankAccCreated()
    {
        if (Player.Instance.IsPlayerHasBankAcc && !promptOverlay.activeSelf)
        {
            SetTutorialContent("Congrats on opening an account! " +
            "\nYou can now deposit and withdraw savings through this bank. " +
            "Try to deposit atleast 500 pesos.");
        DisplayTutorial();
        }
    }
    
    private void HandleSavingDone()
    {
        if (Player.Instance.PlayerBankSavings >= 500 && !animationOverlay.activeSelf)
        {
            SetTutorialContent("Great! You can now start saving money for your future and emergency funds." +
            "\n\nNow, go to a <b><color=#024563>FOODPLACE</color></b> " +
            "and fill up your hunger bar by eating a delicious meal.");
            DisplayTutorial();
        }
    }

    private void HandleFoodEaten()
    {
        if (isFoodEaten && !animationOverlay.activeSelf)
        {
            SetTutorialContent("Brrp! That was a good one. Make sure to always eat on time to " +
            "avoid getting <b><color=red>HOSPITALIZED</color></b> due to hunger!");
            DisplayTutorial();
        }

    }

    private void HandleDoneEating()
    {
        if (isDoneEating && !buildingInteriorOverlay.activeSelf)
        {
            SetTutorialContent("Time to visit the <b><color=#024563>RESIDENTIAL AREA</color></b> "+
            "to explore the <b><color=#024563>Rent Rates</color></b> on all available livings spaces.\n\n"+
            "Tip: Rent the cheapest one and updgrade later on to save money!");
            DisplayTutorial();
        }
    }

    private void HandlePropertyRented()
    {
        if (Player.Instance.CurrentPlayerPlace is not null)
        {
            SetTutorialContent("Congrats on renting your first property! " +
            "Now, click the <b><color=#024563>SLEEP</color></b> button and sleep for atleast 8 hours.");
            DisplayTutorial();
        }
    }

    private void HandleSleepDone()
    {
        if (isSleepDone && !animationOverlay.activeSelf)
        {
            SetTutorialContent("What a good sleep! Make sure to sleep before your energy bar runs out "+
            "to avoid getting <b><color=red>HOSPITALIZED</color></b> due to fatigue.\n\n" +
            "Let's now go back to the city.");
            DisplayTutorial();
        }
    }

    private void HandleLeftSuburbs()
    {
        if (leftSuburbs && !buildingInteriorOverlay.activeSelf)
        {
            SetTutorialContent("Explore different careers and choose the one that suits you best. " +
            "Gain work hours and apply on jobs that matches your current course in the university.\n\n" +
            "For now, go to <b><color=#024563>FOOD XPRESS</color></b> and " +
            "<b><color=#024563>APPLY FOR A JOB</color></b>");
            DisplayTutorial();
        }
    }

    private void HandleJobHired()
    {
        if (isPlayerHired && !promptOverlay.activeSelf)
        {
           SetTutorialContent("Congrats on getting a hired! You can now earn money by working "+
            "a shift for 4 hours. Click the <b><color=#024563>WORK</color></b> button to earn your first salary.");
            DisplayTutorial();
        }
    }

    private void HandleWorkDone()
    {
        if (Player.Instance.CurrentWorkHours >= 4 && isSalaryReceived && !animationOverlay.activeSelf)
        {
            SetTutorialContent("Congrats on earning your first salary! " +
            "You can now use this money to buy food, pay rent, and save for your future.\n\n" +
            "Let's now exit the building and go to the <b><color=#024563>INTELLICASH BANK</color></b> " +
            "to <b><color=#024563>DEPOSIT YOUR SALARY</color></b>.");
            DisplayTutorial();
        }
    }

    private void HandleExitedBuilding()
    {
        if (IsExitedBuilding && !buildingInteriorOverlay.activeSelf)
        {
            SetTutorialContent("Watch out for <b><color=red>UNEXPECTED LIFE EVENTS</color></b> " +
            "such as Earthquakes, Road Accidents, Theft, and Inflation.\n\n" +
            "Tip: Make sure to have enough <b><color=#024563>SAVINGS and EMERGENCY FUNDS</color></b> " +
            "to avoid bankruptcy.");
            DisplayTutorial();
        }
    }

    private void HandleClosing()
    {
        SetTutorialContent("You're good to go! View your missions tracker and " +
            "proceed to the <b><color=#024563>NEXT LEVEL</color></b>.\n\n" +
            " Good luck and enjoy the game!");
        DisplayTutorial();
    }

    private void HandleTutorialDone()
    {
        tutorialStarted = false;
    }

    private void SetTutorialContent(string content)
    {
        tutorialText.text = content;
    }

    private void DisplayTutorial()
    {
        SetUIState(true, true);
        OverlayAnimations.Instance.OpenTutorialAnim(tutorialPopUp);

        nextButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();

        nextButton.onClick.AddListener(OnNextButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void DisplayWelcomeScreen()
    {
        welcomePopUp.SetActive(true);
        tutorialOverlay.SetActive(true);
        OverlayAnimations.Instance.OpenTutorialAnim(welcomePopUp);

        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(OnContinueBtnClicked);
    }

    private void OnNextButtonClick()
    {
        SetUIState(false, false);
        OverlayAnimations.Instance.CloseTutorialAnim(tutorialPopUp, tutorialOverlay);

        // Update state
        currentState = (TutorialState)((int)currentState + 1);

        if (currentState == TutorialState.Completed)
        {
            tutorialStarted = false;
        }
        Debug.Log("Current state: " + currentState);
    }

    private void OnExitButtonClick()
    {
        SetUIState(false, false);
        OverlayAnimations.Instance.CloseTutorialAnim(tutorialPopUp, tutorialOverlay);
        tutorialStarted = false;
    }

    private void OnContinueBtnClicked()
    {
        Debug.Log("Continue button clicked!");
        // Handle Exit Tutorial button click
        tutorialOverlay.SetActive(false);
        welcomePopUp.SetActive(false);

        currentState = (TutorialState)((int)currentState + 1);
        Debug.Log("Current state: " + currentState);
    }

    public void DisableClickToContScreen()
    {
        clickToContOverlay.SetActive(false);
        tutorialOverlay.SetActive(false);

        currentState = (TutorialState)((int)currentState + 1);
    }

}