using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPopUp;
    [SerializeField] private GameObject tutorialOverlay;
    [SerializeField] private GameObject welcomePopUp;
    [SerializeField] private GameObject budgetSetterOverlay;
    [SerializeField] private GameObject smallBotOverlay;
    [SerializeField] private GameObject phoneOverlay;
    [SerializeField] private GameObject missionOverlay;
    [SerializeField] private GameObject buildingInteriorOverlay;
    [SerializeField] private GameObject buildingSelectOverlay;
    [SerializeField] private GameObject animationOverlay;
    [SerializeField] private GameObject promptOverlay;
    [SerializeField] private GameObject clickToContOverlay;
    [SerializeField] private Text tutorialText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button continueButton;

    private bool isBudgetSet = false;
    private bool tutorialStarted = false;
    private bool isPhoneUsed = false;
    private bool isMissionViewed = false;
    private bool isFoodEaten = false;
    private bool isSleepDone = false;
    private bool leftSuburbs = false;
    private bool isPlayerHired = false;
    private bool isSalaryReceived = false;
    private bool isExitedBuilding = false;
    private bool isTutScreenUpdated = false;
    private bool isDoneEating = false;


    private int index = 12;
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
        // Assume that StartTutorial is called from another script to trigger the tutorial
        tutorialOverlay.SetActive(false);
        tutorialPopUp.SetActive(false);
        // StartTutorial();
    }

    public void StartTutorial()
    {
        tutorialStarted = true;
    }

    private void Update()
    {
        if (!tutorialStarted)
        {
            return;
        }

        if (!isBudgetSet && budgetSetterOverlay.activeSelf && index == 1)
        {
            SetTutorialContent("Set your budget for this level. This can help you manage and keep track of your expenses throughout the game." +
                "\nTip: 50% on Expenses, 30% on Consumables, and 20% on Savings and Emergency Funds");

            DisplayTutorial();
        }

        else if (isBudgetSet && index == 2)
        {
            DisplayWelcomeScreen();
        }

        else if (!tutorialOverlay.activeSelf && index == 3)
        {
            SetTutorialContent("Your main goals are to graduate your assigned college degree " +
                "and save 100,000 pesos. To do that, you must manage your finances wisely, " +
                "take care of your health stats, and avoid bankruptcy through all levels.");
            DisplayTutorial();
        }

        else if (!tutorialOverlay.activeSelf && index == 4)
        {
            clickToContOverlay.SetActive(true);
            tutorialOverlay.SetActive(true);
        }

        else if (!clickToContOverlay.activeSelf && index == 5)
        {
            SetTutorialContent("Your stats shown below can decrease and increase while playing. " +
                "\nTo toggle view your stats display, click then unclick a building.");
            DisplayTutorial();
        }

        else if (!tutorialOverlay.activeSelf && smallBotOverlay.activeSelf && index == 6)
        {
            SetTutorialContent("View your life goals, track your finances, buy groceries, " +
            "or call a HeyDay Citizen through your phone. Try using it!");
            DisplayTutorial();
            
        }

        else if (!phoneOverlay.activeSelf && isPhoneUsed && index == 7)
        {
            SetTutorialContent("Well done! Keep track of your current missions using the " +
            "<b><color=#024563>MISSION TRACKER</color></b> shown at the top right of your screen.");
            DisplayTutorial();
        }

        else if (!missionOverlay.activeSelf && isMissionViewed && index == 8)
        {
            SetTutorialContent("Great! Now let's start accomplishing each mission! " +
            "\n\nGo to <b><color=#024563>INTELLICASH BANK</color></b> " +
            "to <b><color=#024563>OPEN A SAVINGS ACCOUNT</color></b> to start saving money.");
            DisplayTutorial();
        }

        else if (Player.Instance.IsPlayerHasBankAcc && !promptOverlay.activeSelf && index == 9)
        {
            SetTutorialContent("Congrats on opening an account! " +
            "\nYou can now deposit and withdraw savings through this bank. " +
            "Try to deposit atleast 500 pesos.");
            DisplayTutorial();
        }

        else if (Player.Instance.PlayerBankSavings >= 500 && !animationOverlay.activeSelf && index == 10)
        {
            SetTutorialContent("Excellent! Let's now proceed to the next mission. " +
            "Go to the <b><color=#024563>UNIVERSITY</color></b> and enroll on your " +
            "<b><color=#024563>GOAL COURSE</color></b> \n" +
            "\n Tip: Your goal course can be viewed in the Goal Tracker App in your phone.");
            DisplayTutorial();
        }

        else if (Player.Instance.PlayerEnrolledCourse == Player.Instance.GoalCourse && index == 11)
        {
            SetTutorialContent("Congrats on getting officially enrolled! " +
            "Now, click the <b><color=#024563>STUDY</color></b> button and study for atleast 4 hours.");
            DisplayTutorial();
        }

        else if (Player.Instance.PlayerStudyHours >= 4 && !animationOverlay.activeSelf && index == 12)
        {
            SetTutorialContent("Excellent! You've got a long way to go but you'll get there! " +
            "\n\nLet's now exit the building and go to a <b><color=#024563>FOODPLACE</color></b> " +
            "and fill up your hunger bar by eating a delicious meal.");
            DisplayTutorial();
        }

        else if (isFoodEaten && !animationOverlay.activeSelf && index == 13)
        {
            SetTutorialContent("Brrp! That was a good one. Make sure to always eat on time to " +
            "avoid getting <b><color=red>HOSPITALIZED</color></b> due to hunger!");
            DisplayTutorial();
        }

        else if (isDoneEating && index == 14)
        {
            SetTutorialContent("Time to visit the <b><color=#024563>RESIDENTIAL AREA</color></b> "+
            "to explore the <b><color=#024563>Rent Rates</color></b> on all available livings spaces.\n\n"+
            "Tip: Rent the cheapest one and updgrade later on to save money!");
            DisplayTutorial();
        }

        else if (Player.Instance.CurrentPlayerPlace is not null && index == 15)
        {
            SetTutorialContent("Congrats on renting a property! Here, you can regain energy by clicking the SLEEP button. "+
            "Try sleeping for atleast 4 hours.");
            DisplayTutorial();
        }

        else if (isSleepDone && !animationOverlay.activeSelf && index == 16)
        {
            SetTutorialContent("What a good sleep! Make sure to sleep before your energy bar runs out "+
            "to avoid getting <b><color=red>HOSPITALIZED</color></b> due to fatigue.\n\n" +
            "Let's now go back to the city.");
            ;
            DisplayTutorial();
        }

        else if (leftSuburbs && index == 17)
        {
            SetTutorialContent("Explore different careers and choose the one that suits you best. " +
            "Gain work hours and apply on jobs that matches your current course in the university.\n\n" +
            "For now, go to <b><color=#024563>FOOD XPRESS</color></b> and " +
            "<b><color=#024563>APPLY FOR A JOB</color></b>");
            ;
            DisplayTutorial();
        }

        else if (Player.Instance.CurrentPlayerJob is not null && isPlayerHired && index == 18)
        {
            SetTutorialContent("Congrats on getting a hired! You can now earn money by working "+
            "a shift for 4 hours. Click the <b><color=#024563>WORK</color></b> button to earn your first salary.");
            DisplayTutorial();
        }

        else if (Player.Instance.CurrentWorkHours >= 4 && !animationOverlay.activeSelf && index == 19)
        {
            Debug.Log("index: " + index);
            Debug.Log(isSalaryReceived);
            SetTutorialContent("Congrats on receiving your first salary! Remember to take a break every now " +
            "and then. Go to the cinema, party, or make a purchase in the Mall to increase your happiness " +
            "to avoid getting <b><color=red>HOSPITALIZED</color></b> due to depression.");
            DisplayTutorial();
        }

        else if (!isTutScreenUpdated && !buildingInteriorOverlay.activeSelf && index == 20)
        {
            SetTutorialContent("Watch out for <b><color=red>UNEXPECTED LIFE EVENTS</color></b> " +
            "such as Earthquakes, Road Accidents, Theft, and Inflation.\n\n" +
            "Tip: Make sure to have enough <b><color=#024563>SAVINGS and EMERGENCY FUNDS</color></b> " +
            "to avoid bankruptcy.");
            DisplayTutorial();
        }

        else if (index == 21)
        {
            SetTutorialContent("You're good to go! View your missions tracker and " +
            "proceed to the <b><color=#024563>NEXT LEVEL</color></b>.\n\n" +
            " Good luck and enjoy the game!");
            DisplayTutorial();
        }

        // if (isTutorialDone)
        // {
        //     tutorialStarted = false;
        // }
    }

    public void DisableClickToContScreen()
    {
        clickToContOverlay.SetActive(false);
        tutorialOverlay.SetActive(false);
        isTutScreenUpdated = false;
        index = 5;
    }

    private void SetTutorialContent(string content)
    {
        tutorialText.text = content;
    }

    private void DisplayTutorial()
    {
        // Remove existing listeners before adding new ones
        nextButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();

        tutorialPopUp.SetActive(true);
        tutorialOverlay.SetActive(true);
        OverlayAnimations.Instance.OpenTutorialAnim(tutorialPopUp);
        isTutScreenUpdated = true;
        Debug.Log("index: " + index);

        nextButton.onClick.AddListener(OnNextButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void DisplayWelcomeScreen()
    {
        continueButton.onClick.RemoveAllListeners();
        welcomePopUp.SetActive(true);
        tutorialOverlay.SetActive(true);
        OverlayAnimations.Instance.OpenTutorialAnim(welcomePopUp);
        
        continueButton.onClick.AddListener(OnContinueBtnClicked);
    }

    private void OnNextButtonClick()
    {
        // Handle Next button click
        tutorialPopUp.SetActive(false);
        tutorialOverlay.SetActive(false);
        OverlayAnimations.Instance.CloseTutorialAnim(tutorialPopUp, tutorialOverlay);

        isTutScreenUpdated = false;
        index++;
        Debug.Log("INDEX CLICK BTN: " + index);

        if (index == 22)
        {
            tutorialStarted = false;
        }
    }

    private void OnExitButtonClick()
    {
        // Handle Exit Tutorial button click
        tutorialPopUp.SetActive(false);
        tutorialOverlay.SetActive(false);
        OverlayAnimations.Instance.CloseTutorialAnim(tutorialPopUp, tutorialOverlay);
        tutorialStarted = false;
    }

    private void OnContinueBtnClicked()
    {
        // Handle Exit Tutorial button click
        tutorialOverlay.SetActive(false);
        welcomePopUp.SetActive(false);
        index = 3;
        
    }

}