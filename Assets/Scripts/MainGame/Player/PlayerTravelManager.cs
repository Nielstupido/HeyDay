using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTravelManager : MonoBehaviour
{
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject travelingOverlay;
    [SerializeField] private Player3dController player3DController;
    private Building currentVisitedBuilding;
    public Building CurrentVisitedBuilding { get{return currentVisitedBuilding;}}
    public static PlayerTravelManager Instance { get; private set; }


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


    public void PlayerTravel(Building selectedBuilding)
    {
        StartCoroutine(StartTravelingOverlay(2f, selectedBuilding));
    }


    private void MovePlayerModel(Building selectedBuilding)
    {
        Debug.Log("PlayerModel Position Before: " + playerModel.transform.position);
        player3DController.playerNavMesh.isStopped = true;
        playerModel.gameObject.transform.position = selectedBuilding.transform.GetChild(selectedBuilding.transform.childCount - 1).transform.position;
        Debug.Log(selectedBuilding.transform.name);
        Debug.Log(selectedBuilding.transform.GetChild(selectedBuilding.transform.childCount - 1).transform.position);
        Debug.Log("PlayerModel Position After: " + playerModel.transform.position);
        currentVisitedBuilding = selectedBuilding;
    }


    private IEnumerator StartTravelingOverlay(float travelingTime, Building selectedBuilding)
    {
        travelingOverlay.SetActive(true);
        MovePlayerModel(selectedBuilding);
        yield return new WaitForSeconds(travelingTime);
        travelingOverlay.SetActive(false);
        yield return null;
    }
}
