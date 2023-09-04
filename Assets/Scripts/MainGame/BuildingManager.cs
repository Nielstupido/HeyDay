using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private IDictionary<string, GameObject> buildings = new Dictionary<string, GameObject>();
    [SerializeField] private GameObject buildingSelectOverlay;
    [SerializeField] private List<Sprite> buttonImages = new List<Sprite>();
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject walkBtn;
    [SerializeField] private GameObject rideBtn; 
    [SerializeField] private GameObject enterBtn;
    [SerializeField] private Transform buttonsHolder;
    [SerializeField] private Player player;
    [SerializeField] private GameObject btnPrefab;
    private Building currentSelectedBuilding;


    public Building CurrentSelectedBuilding { set{currentSelectedBuilding = value;} get{return currentSelectedBuilding;}}
    public GameObject BuildingSelectOverlay { get{return buildingSelectOverlay;}}


    private void Start()
    {
        enterBtn.GetComponent<Button>().onClick.AddListener(delegate { EnterBuilding(currentSelectedBuilding); });
        buildingSelectOverlay.SetActive(false);
    }


    public void Walk()
    {
        player.Walk(10f);
        walkBtn.SetActive(false);
        rideBtn.SetActive(false);
        enterBtn.SetActive(true);
    }


    public void Ride()
    {
        player.Walk(5f);
        walkBtn.SetActive(false);
        rideBtn.SetActive(false);
        enterBtn.SetActive(true);
    }


    public void EnterBuilding(Building selectedBuilding)
    {
        gameManager.EnterBuilding();
        PrepareButtons(selectedBuilding);
    }


    public void ExitBuilding()
    {
        for (var i = buttonsHolder.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(buttonsHolder.GetChild(i).gameObject);
        }
        walkBtn.SetActive(true);
        rideBtn.SetActive(true);
        enterBtn.SetActive(false);
        gameManager.ExitBuilding();
    }


    public void PrepareButtons(Building selectedBuilding)
    {
        foreach(Buttons btn in selectedBuilding.actionButtons)
        {
            GameObject newBtn = Instantiate(btnPrefab, Vector3.zero, Quaternion.identity, buttonsHolder);
            newBtn.GetComponent<Image>().sprite = buttonImages[((int)btn)];
        }
    }
}
