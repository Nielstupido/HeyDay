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

        GameManager.onGameStarted += LoadGameData;
        GameManager.onSaveGameStateData += SaveGameData;
    }


    private void OnDestroy()
    {
        GameManager.onGameStarted -= LoadGameData;
        GameManager.onSaveGameStateData -= SaveGameData;
    }


    private void LoadGameData()
    {
        foreach (Building building in BuildingManager.Instance.AllBuildings)
        {
            if (GameManager.Instance.CurrentGameStateData.currentVisitedBuilding == building.buildingStringName)
            {
                this.currentVisitedBuilding = building;
            }
        }

        if (this.currentVisitedBuilding != null)
        {
            MovePlayerModel(true, this.currentVisitedBuilding);
        }
    }


    private void SaveGameData()
    {
        if (this.currentVisitedBuilding != null)
        {
            GameManager.Instance.CurrentGameStateData.currentVisitedBuilding = this.currentVisitedBuilding.buildingStringName;
        }
    }


    public void PlayerTravel(Building selectedBuilding, ModeOfTravels modeOfTravel, ActionAnimations actionAnimation)
    {
        currentModeOfTravel = modeOfTravel;

        if (modeOfTravel == ModeOfTravels.WALK)
        {
            StartCoroutine(StartTravelingOverlay(false, 2f, selectedBuilding, actionAnimation));
        }
        else
        {
            StartCoroutine(StartTravelingOverlay(true, 2f, selectedBuilding, actionAnimation));
        }
    }


    public void MovePlayerModel(bool directlyMove, Building selectedBuilding)
    {
        if (directlyMove)
        {
            Player3dController.Instance.StopMovement();      
            playerModel.gameObject.transform.position = selectedBuilding.transform.GetChild(selectedBuilding.transform.childCount - 1).transform.position;
        }
        else
        {
            Player3dController.Instance.WalkToPoint(selectedBuilding.transform.GetChild(selectedBuilding.transform.childCount - 1).transform.position);
        }

        currentVisitedBuilding = selectedBuilding;
        PlayerActionObservers.onPlayerTraveled(currentModeOfTravel);
    }


    private IEnumerator StartTravelingOverlay(bool directlyMove, float travelingTime, Building selectedBuilding, ActionAnimations actionAnimation)
    {
        AnimOverlayManager.Instance.StartAnim(actionAnimation);
        MovePlayerModel(directlyMove, selectedBuilding);
        yield return new WaitForSeconds(travelingTime);
        AnimOverlayManager.Instance.StopAnim();
        AudioManager.Instance.StopMusicEffect();
        yield return null;
    }
}
