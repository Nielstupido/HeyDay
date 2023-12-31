using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject buildingSelectOverlay;
    [SerializeField] private GameObject cinemaOverlay;
    [SerializeField] private GameObject barOverlay;
    [SerializeField] private GameObject resViewHUD;
    [SerializeField] private Text buildingNameText;
    [SerializeField] private Text buildingDescriptionText;
    [SerializeField] private Text buildingOpeningHrs;
    [SerializeField] private List<Sprite> buttonImages = new List<Sprite>();
    [SerializeField] private GameObject walkBtn;
    [SerializeField] private GameObject rideBtn; 
    [SerializeField] private GameObject enterBtn;
    [SerializeField] private GameObject closedBtn;
    [SerializeField] private Transform buttonsHolder;
    [SerializeField] private GameObject btnPrefab;
    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject camera2;
    [SerializeField] private GameObject buildingInteriorOverlay;
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private Transform npcHolder;
    [SerializeField] private PlayerTravelManager playerTravelManager;
    [SerializeField] private CarCatalogueManager carCatalogueManager;
    [SerializeField] private SwitchMenuItem switchMenuItem;
    [SerializeField] private MallManager mallManager;
    [SerializeField] private BoxCollider resAreaCollider;
    [SerializeField] private Prompts notEnoughMoneyFare;
    [SerializeField] private List<BoxCollider> resBuildingColliders = new List<BoxCollider>();
    [SerializeField] private List<Building> allBuildings = new List<Building>();

    public List<Building> AllBuildings { get{return allBuildings;}}
    private Building currentSelectedBuilding;
    private BuildingSelect buildingSelectCopy;
    public delegate void OnBuildingBtnClicked(Buttons clickedBtn);
    public OnBuildingBtnClicked onBuildingBtnClicked;
    public Building CurrentSelectedBuilding { set{currentSelectedBuilding = value;} get{return currentSelectedBuilding;}}
    public GameObject BuildingSelectOverlay { get{return buildingSelectOverlay;}}
    public GameObject WalkBtn { get{return walkBtn;}}
    public GameObject RideBtn { get{return rideBtn;}}
    public GameObject EnterBtn { get{return enterBtn;}}
    public GameObject ClosedBtn { get{return closedBtn;}}
    public List<Sprite> ButtonImages { get{return buttonImages;}}
    public string BuildingNameText { set{buildingNameText.text = value;}}
    public string BuildingDescriptionText { set{buildingDescriptionText.text = value;}}
    public string BuildingOpeningHrs { set{buildingOpeningHrs.text = value;}}
    public BuildingSelect BuildingSelectCopy { set{buildingSelectCopy = value;}}
    public static BuildingManager Instance { get; private set; }


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
        
        TimeManager.onTimeAdded += CheckBuildinClosingTime;
    }


    private void OnDestroy()
    {
        TimeManager.onTimeAdded -= CheckBuildinClosingTime;
    }


    private void Start()
    {
        camera2.SetActive(false);
        enterBtn.GetComponent<Button>().onClick.AddListener( () => { EnterBuilding(currentSelectedBuilding); } );
        buildingSelectOverlay.SetActive(false);

        foreach (BoxCollider boxCollider in resBuildingColliders)
        {
            boxCollider.enabled = false;
        }
    }


    private void CheckBuildinClosingTime(float currentTime)
    {
        if (currentSelectedBuilding != null)
        {
            if ((currentTime > currentSelectedBuilding.buildingClosingTime || currentTime < CurrentSelectedBuilding.buildingOpeningTime) && buildingInteriorOverlay.activeSelf 
                    && currentSelectedBuilding.buildingOpeningTime != 0f && currentSelectedBuilding.buildingClosingTime != 0f)
            {
                ExitBuilding();
            }
        }
    }


    public void Walk()
    {
        AudioManager.Instance.PlaySFX("Select");
        Player.Instance.Walk(5f, 3f);
        walkBtn.SetActive(false);
        rideBtn.SetActive(false);
        enterBtn.SetActive(true);
        playerTravelManager.PlayerTravel(currentSelectedBuilding, ModeOfTravels.WALK, ActionAnimations.WALK);
        LevelManager.onFinishedPlayerAction(MissionType.WALK);
        LifeEventsManager.Instance.StartLifeEvent(LifeEvents.ROBBERY);
        AudioManager.Instance.PlaySFX("Walk");
    }


    public void Ride()
    {
        AudioManager.Instance.PlaySFX("Select");
        if (Player.Instance.PlayerOwnedVehicles.Count == 0)
        {
            if (!Player.Instance.Pay(false, 13f, 0.1f, 0f, 1f, notEnoughMoneyFare, 5f))
            {
                return;
            }

            playerTravelManager.PlayerTravel(currentSelectedBuilding, ModeOfTravels.COMMUTE, ActionAnimations.COMMUTE);
            LevelManager.onFinishedPlayerAction(MissionType.COMMUTE);
            LifeEventsManager.Instance.StartLifeEvent(LifeEvents.ACCIDENT);
            AudioManager.Instance.PlaySFX("Commute");
        }
        else
        {
            Player.Instance.Pay(false, 0f, 0.05f, 0f, 0.5f, notEnoughMoneyFare, 2f);
            playerTravelManager.PlayerTravel(currentSelectedBuilding, ModeOfTravels.DRIVE, ActionAnimations.DRIVE);
            AudioManager.Instance.PlaySFX("Drive");
        }

        walkBtn.SetActive(false);
        rideBtn.SetActive(false);
        enterBtn.SetActive(true);
    }


    public void EnterBuilding(Building selectedBuilding)
    {
        AudioManager.Instance.PlaySFX("Select");
        AudioManager.Instance.StopMusic();
        GameUiController.onScreenOverlayChanged(UIactions.SHOW_SMALL_BOTTOM_OVERLAY);

        if (selectedBuilding.buildingEnumName == Buildings.RESIDENTIAL)
        {
            EnterResidentialArea();
        }
        else
        {
            buildingInteriorOverlay.SetActive(true);
            PrepareButtons(selectedBuilding);
            PrepareNpc();
        }

        if (selectedBuilding.bbuildingBgSound)
        {
            AudioManager.Instance.PlayMusicEffect(selectedBuilding.bbuildingBgSound);
        }
        
        LevelManager.onFinishedPlayerAction(MissionType.VISIT, interactedBuilding:selectedBuilding.buildingEnumName);
    }


    public void ExitBuilding()
    {
        AudioManager.Instance.StopMusicEffect();
        AudioManager.Instance.PlayMusic("Theme2");
        RemoveBuildingActionBtns();
        RemoveNpc();
        UniversityManager.Instance.OnExitedUniversity();
        currentSelectedBuilding.actionButtons.Clear();
        buildingInteriorOverlay.SetActive(false);
        buildingSelectCopy.RefreshSelectOverlayUI();
        TutorialManager.Instance.IsExitedBuilding = true;
    }


    public void PrepareButtons(Building selectedBuilding)
    {
        buildingInteriorOverlay.GetComponent<Image>().sprite = selectedBuilding.buildingBgImage;
        selectedBuilding.CheckButtons();
        RemoveBuildingActionBtns();
        
        foreach(Buttons btn in selectedBuilding.actionButtons)
        {
            GameObject newBtn = Instantiate(btnPrefab, Vector3.zero, Quaternion.identity, buttonsHolder);
            newBtn.GetComponent<Image>().sprite = buttonImages[((int)btn)];
            newBtn.GetComponent<Button>().onClick.AddListener( () => {onBuildingBtnClicked(btn);} );
        }
    }


    public void PrepareNpc()
    {
        RemoveNpc();
        
        foreach(CharactersScriptableObj character in GameManager.Instance.Characters)
        {
            if (MeetUpSystem.Instance.CheckForPendingMeetup())
            {
                if (MeetUpSystem.Instance.GetMeetupDets().Item1 == currentSelectedBuilding && MeetUpSystem.Instance.GetMeetupDets().Item2 == character)
                {
                    InstantiateCharacter(character);
                    continue;
                }
            }

            if (character.currentBuildiing == currentSelectedBuilding.buildingEnumName)
            {
                InstantiateCharacter(character);
            }
        }
    } 


    private void InstantiateCharacter(CharactersScriptableObj character)
    {
        GameObject newNpc = Instantiate(npcPrefab, Vector3.zero, Quaternion.identity, npcHolder);
        newNpc.GetComponent<CharactersObj>().SetupCharacter(character, false);
        newNpc.GetComponent<Button>().onClick.AddListener( () => {GameManager.Instance.InteractWithNPC(character.characterName);;});
    }


    private void RemoveNpc()
    {
        for (var i = 0; i < npcHolder.childCount; i++)
        {
            Object.Destroy(npcHolder.GetChild(i).gameObject);
        }
    }


    private void RemoveBuildingActionBtns()
    {
        for (var i = 0; i < buttonsHolder.childCount; i++)
        {
            Object.Destroy(buttonsHolder.GetChild(i).gameObject);
        }
    }


    public void EnterResidentialArea()
    {
        AudioManager.Instance.PlaySFX("Select");
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic("Residential");
        GameManager.Instance.UpdateBottomOverlay(UIactions.SHOW_SMALL_BOTTOM_OVERLAY);
        buildingSelectOverlay.SetActive(false);
        resViewHUD.SetActive(true);
        camera2.SetActive(true);
        camera1.SetActive(false);
        resAreaCollider.enabled = false;

        foreach (BoxCollider boxCollider in resBuildingColliders)
        {
            boxCollider.enabled = true;
        }
    }


    public void ExitResidential()
    {
        AudioManager.Instance.PlaySFX("Select");
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic("Theme2");
        ResBuildingManager.Instance.ResBuildingSelectOverlay.SetActive(false);
        GameManager.Instance.UpdateBottomOverlay(UIactions.SHOW_DEFAULT_BOTTOM_OVERLAY);
        TutorialManager.Instance.LeftSuburbs = true;
        camera1.SetActive(true);
        resViewHUD.SetActive(false);
        camera2.SetActive(false);
        resAreaCollider.enabled = true;

        foreach (BoxCollider boxCollider in resBuildingColliders)
        {
            boxCollider.enabled = false;
        }
    }


    public void OpenCarCatalogueOverlay()
    {
        carCatalogueManager.ShowCarCatalogue();
    }


    public void OpenConsumablesOverlay()
    {
        switchMenuItem.ShowConsumablesMenu();
    }


    public void OpenMallOverlay()
    {
        mallManager.ShowMallOverlay();
    }

    
    public void OpenUniversityEnrollOverlay()
    {
        UniversityManager.Instance.ShowEnrollOverlay();
    }   


    public void OpenUniversityStudyOverlay()
    {
        UniversityManager.Instance.ShowStudyOverlay();
    }   


    public void OpenCinemaOverlay()
    {
        AudioManager.Instance.PlaySFX("Select");
        cinemaOverlay.SetActive(true);
        OverlayAnimations.Instance.ShowMovieTicket();
    }


    public void OpenBarOverlay()
    {
        AudioManager.Instance.PlaySFX("Select");
        barOverlay.SetActive(true);
        OverlayAnimations.Instance.ShowBarTicket();
    }
}
