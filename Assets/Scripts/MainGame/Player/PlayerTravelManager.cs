using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ModeOfTravels
{
    NA,
    WALK,
    COMMUTE,
    DRIVE
}


public class PlayerActionObservers : MonoBehaviour
{
    public delegate void OnPlayerTraveled(ModeOfTravels modeOfTravel);
    public static OnPlayerTraveled onPlayerTraveled;
}


public class PlayerTravelManager : MonoBehaviour
{
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Player3dController player3DController;
    private Building currentVisitedBuilding;
    private ModeOfTravels currentModeOfTravel;
    public Building CurrentVisitedBuilding { get{return currentVisitedBuilding;}}
    public static PlayerTravelManager Instance { get; private set; }


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


    public void PlayerTravel(Building selectedBuilding, ModeOfTravels modeOfTravel, ActionAnimations actionAnimation)
    {
        currentModeOfTravel = modeOfTravel;
        StartCoroutine(StartTravelingOverlay(2f, selectedBuilding, actionAnimation));
    }


    public void MovePlayerModel(Building selectedBuilding)
    {
        // player3DController.playerNavMesh.isStopped = true;
        playerModel.gameObject.transform.position = selectedBuilding.transform.GetChild(selectedBuilding.transform.childCount - 1).transform.position;
        currentVisitedBuilding = selectedBuilding;
        PlayerActionObservers.onPlayerTraveled(currentModeOfTravel);
    }


    private IEnumerator StartTravelingOverlay(float travelingTime, Building selectedBuilding, ActionAnimations actionAnimation)
    {
        AnimOverlayManager.Instance.StartAnim(actionAnimation);
        MovePlayerModel(selectedBuilding);
        yield return new WaitForSeconds(travelingTime);
        AnimOverlayManager.Instance.StopAnim();
        AudioManager.Instance.StopMusicEffect();
        yield return null;
    }
}
