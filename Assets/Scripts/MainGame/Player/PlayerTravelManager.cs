using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTravelManager : MonoBehaviour
{
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Player3dController player3DController;
    [SerializeField] private BuildingSelect buildingSelect;
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
        player3DController.playerNavMesh.isStopped = true;
        playerModel.gameObject.transform.position = selectedBuilding.transform.GetChild(selectedBuilding.transform.childCount - 1).transform.position;
        currentVisitedBuilding = selectedBuilding;
        buildingSelect.RefreshSelectOverlayUI();
    }


    private IEnumerator StartTravelingOverlay(float travelingTime, Building selectedBuilding)
    {
        AnimOverlayManager.Instance.StartAnim(Animations.TRAVELING);
        MovePlayerModel(selectedBuilding);
        yield return new WaitForSeconds(travelingTime);
        AnimOverlayManager.Instance.StopAnim();
        yield return null;
    }
}
