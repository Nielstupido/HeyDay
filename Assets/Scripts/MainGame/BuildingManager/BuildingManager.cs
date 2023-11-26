using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private IDictionary<string, GameObject> buildings = new Dictionary<string, GameObject>();
    [SerializeField] private GameObject buildingSelectOverlay;
    [SerializeField] private Text buildingNameText;
    [SerializeField] private List<Sprite> buttonImages = new List<Sprite>();
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject walkBtn;
    [SerializeField] private GameObject rideBtn; 
    [SerializeField] private GameObject enterBtn;
    [SerializeField] private GameObject closedBtn;
    [SerializeField] private Transform buttonsHolder;
    [SerializeField] private GameObject btnPrefab;
    [SerializeField] private GameObject smallStatsOverlay;
    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject camera2;
    [SerializeField] private GameObject buildingInteriorOverlay;
    [SerializeField] private PlayerTravelManager playerTravelManager;
    [SerializeField] private CarCatalogueManager carCatalogueManager;
    [SerializeField] private SwitchMenuItem switchMenuItem;
    [SerializeField] private MallManager mallManager;
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
    public static BuildingManager Instance { get; private set; }


    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }


    private void Start()
    {
        camera2.SetActive(false);
        enterBtn.GetComponent<Button>().onClick.AddListener( () => { EnterBuilding(currentSelectedBuilding); } );
        buildingSelectOverlay.SetActive(false);
    }


    public void Walk()
    {
        Player.Instance.Walk(10f);
        walkBtn.SetActive(false);
        rideBtn.SetActive(false);
        enterBtn.SetActive(true);
        playerTravelManager.PlayerTravel(currentSelectedBuilding, ModeOfTravels.WALK);
        LevelManager.onFinishedPlayerAction(MissionType.WALK);
    }


    public void Ride()
    {
        Player.Instance.Ride(5f);
        walkBtn.SetActive(false);
        rideBtn.SetActive(false);
        enterBtn.SetActive(true);
        playerTravelManager.PlayerTravel(currentSelectedBuilding, ModeOfTravels.RIDE);
        LevelManager.onFinishedPlayerAction(MissionType.COMMUTE);
    }


    public void EnterBuilding(Building selectedBuilding)
    {
        GameUiController.onScreenOverlayChanged(UIactions.SHOW);

        if (selectedBuilding.buildingEnumName == Buildings.RESIDENTIAL)
        {
            EnterResidentialArea();
        }
        else
        {
            buildingInteriorOverlay.SetActive(true);
            PrepareButtons(selectedBuilding);
        }

        LevelManager.onFinishedPlayerAction(MissionType.VISIT, interactedBuilding:selectedBuilding.buildingEnumName);
    }


    public void ExitBuilding()
    {
        RemoveBuildingActionBtns();
        currentSelectedBuilding.actionButtons.Clear();
        smallStatsOverlay.SetActive(false);
        buildingInteriorOverlay.SetActive(false);
        GameUiController.onScreenOverlayChanged(UIactions.HIDE);
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


    private void RemoveBuildingActionBtns()
    {
        for (var i = buttonsHolder.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(buttonsHolder.GetChild(i).gameObject);
        }
    }


    public void EnterResidentialArea()
    {
        buildingSelectOverlay.SetActive(false);
        camera2.SetActive(true);
        camera1.SetActive(false);
    }


    public void ExitResidential()
    {
        camera1.SetActive(true);
        smallStatsOverlay.SetActive(false);
        camera2.SetActive(false);
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
}
