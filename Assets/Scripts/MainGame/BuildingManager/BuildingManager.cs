using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Dictionary<string, GameObject> buildings = new Dictionary<string, GameObject>();
    [SerializeField] private GameObject buildingSelectOverlay;
    [SerializeField] private GameObject cinemaOverlay;
    [SerializeField] private GameObject barOverlay;
    [SerializeField] private GameObject resViewHUD;
    [SerializeField] private Text buildingNameText;
    [SerializeField] private Text buildingDescriptionText;
    [SerializeField] private Text buildingOpeningHrs;
    [SerializeField] private List<Sprite> buttonImages = new List<Sprite>();
    [SerializeField] private GameManager gameManager;
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
    [SerializeField] private List<BoxCollider> resBuildingColliders = new List<BoxCollider>();
    private Building currentSelectedBuilding;
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


    public void Walk()
    {
        Player.Instance.Walk(5f, 3f);
        walkBtn.SetActive(false);
        rideBtn.SetActive(false);
        enterBtn.SetActive(true);
        playerTravelManager.PlayerTravel(currentSelectedBuilding, ModeOfTravels.WALK, ActionAnimations.WALK);
        LevelManager.onFinishedPlayerAction(MissionType.WALK);
        LifeEventsManager.Instance.StartLifeEvent(LifeEvents.ROBBERY);
    }


    public void Ride()
    {
        Player.Instance.Ride(2f, 1f);
        walkBtn.SetActive(false);
        rideBtn.SetActive(false);
        enterBtn.SetActive(true);

        if (Player.Instance.PlayerOwnedVehicles.Count == 0)
        {
            playerTravelManager.PlayerTravel(currentSelectedBuilding, ModeOfTravels.COMMUTE, ActionAnimations.COMMUTE);
            LevelManager.onFinishedPlayerAction(MissionType.COMMUTE);
            LifeEventsManager.Instance.StartLifeEvent(LifeEvents.ACCIDENT);
        }
        else
        {
            playerTravelManager.PlayerTravel(currentSelectedBuilding, ModeOfTravels.DRIVE, ActionAnimations.DRIVE);
        }
    }


    public void EnterBuilding(Building selectedBuilding)
    {
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

        LevelManager.onFinishedPlayerAction(MissionType.VISIT, interactedBuilding:selectedBuilding.buildingEnumName);
    }


    public void ExitBuilding()
    {
        RemoveBuildingActionBtns();
        RemoveNpc();
        UniversityManager.Instance.OnExitedUniversity();
        currentSelectedBuilding.actionButtons.Clear();
        buildingInteriorOverlay.SetActive(false);
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
        ResBuildingManager.Instance.ResBuildingSelectOverlay.SetActive(false);
        GameManager.Instance.UpdateBottomOverlay(UIactions.SHOW_DEFAULT_BOTTOM_OVERLAY);
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
        cinemaOverlay.SetActive(true);
    }


    public void OpenBarOverlay()
    {
        barOverlay.SetActive(true);
    }
}
