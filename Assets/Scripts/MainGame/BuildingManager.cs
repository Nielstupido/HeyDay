using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private IDictionary<string, GameObject> buildings = new Dictionary<string, GameObject>();
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject walkBtn;
    [SerializeField] private GameObject rideBtn;
    [SerializeField] private GameObject enterBtn;
    [SerializeField] private Player player;
    private string currentSelectedBuilding;


    public string CurrentSelectedBuilding { set{currentSelectedBuilding = value;} get{return currentSelectedBuilding;}}


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


    public void EnterBuilding()
    {
        gameManager.EnterBuilding();
    }


    public void ExitBuilding()
    {
        walkBtn.SetActive(true);
        rideBtn.SetActive(true);
        enterBtn.SetActive(false);
        gameManager.ExitBuilding();
    }
}
