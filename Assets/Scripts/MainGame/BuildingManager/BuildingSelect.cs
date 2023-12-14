using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuildingSelect : MonoBehaviour, IPointerClickHandler
{
    private Vector3 scaleDown = new Vector3(0.1f, 0.1f, 0.1f);


    private void Start()
    {
        BuildingManager.Instance.BuildingSelectOverlay.transform.localScale = scaleDown;
        PlayerActionObservers.onPlayerTraveled += RefreshSelectOverlayUI;
    }


    private void OnDestroy()
    {
        PlayerActionObservers.onPlayerTraveled -= RefreshSelectOverlayUI;
    }

    
        private void OnEnable()
    {
        PlayerActionObservers.onPlayerTraveled += RefreshSelectOverlayUI;
    }


    private void OnDisable()
    {
        PlayerActionObservers.onPlayerTraveled -= RefreshSelectOverlayUI;
    }

    public void RefreshSelectOverlayUI(ModeOfTravels modeOfTravel = ModeOfTravels.NA)
    {
        if (PlayerTravelManager.Instance.CurrentVisitedBuilding != BuildingManager.Instance.CurrentSelectedBuilding)
        {
            BuildingManager.Instance.WalkBtn.SetActive(true);
            BuildingManager.Instance.RideBtn.SetActive(true);
            BuildingManager.Instance.EnterBtn.SetActive(false);
            BuildingManager.Instance.ClosedBtn.SetActive(false);
        }
        else
        {
            BuildingManager.Instance.WalkBtn.SetActive(false);
            BuildingManager.Instance.RideBtn.SetActive(false);

            if (CheckIsBuildingOpen(PlayerTravelManager.Instance.CurrentVisitedBuilding))
            {
                BuildingManager.Instance.ClosedBtn.SetActive(false);
                BuildingManager.Instance.EnterBtn.SetActive(true);
            }
            else
            {
                BuildingManager.Instance.EnterBtn.SetActive(false);
                BuildingManager.Instance.ClosedBtn.SetActive(true);
            }
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Building currentSelectedBuilding = eventData.selectedObject.GetComponentInParent<Building>();

        if (BuildingManager.Instance.BuildingSelectOverlay.activeSelf && BuildingManager.Instance.CurrentSelectedBuilding == currentSelectedBuilding)
        {
            AnimationManager.ScaleObj(BuildingManager.Instance.BuildingSelectOverlay, scaleDown, 0.4f, true, LeanTweenType.easeInOutBack);
            GameUiController.onScreenOverlayChanged(UIactions.SHOW_DEFAULT_BOTTOM_OVERLAY);
        }
        else
        {
            BuildingManager.Instance.BuildingNameText = currentSelectedBuilding.buildingStringName;
            BuildingManager.Instance.BuildingDescriptionText = currentSelectedBuilding.buildingDescription;
            BuildingManager.Instance.BuildingOpeningHrs = TimeManager.Instance.TransposeTimeValue((int)currentSelectedBuilding.buildingOpeningTime).ToString() + " " + 
                                                        TimeManager.Instance.AmOrPm((int)currentSelectedBuilding.buildingOpeningTime).ToString() + " - " +
                                                        TimeManager.Instance.TransposeTimeValue((int)currentSelectedBuilding.buildingClosingTime).ToString() + " " + 
                                                        TimeManager.Instance.AmOrPm((int)currentSelectedBuilding.buildingClosingTime).ToString();
            BuildingManager.Instance.CurrentSelectedBuilding = currentSelectedBuilding;
            RefreshSelectOverlayUI(); 

            if (!BuildingManager.Instance.BuildingSelectOverlay.activeSelf)
            {
                BuildingManager.Instance.BuildingSelectOverlay.SetActive(true);
                AnimationManager.ScaleObj(BuildingManager.Instance.BuildingSelectOverlay, Vector3.one, 0.4f, false, LeanTweenType.easeOutBounce);
                GameUiController.onScreenOverlayChanged(UIactions.SHOW_SMALL_BOTTOM_OVERLAY);
            }
        }
    }


    private bool CheckIsBuildingOpen(Building currentBuilding)
    {
        if (currentBuilding.buildingOpeningTime == 0 && currentBuilding.buildingClosingTime == 0)
        {
            return true;
        }
        
        if (currentBuilding.buildingOpeningTime > currentBuilding.buildingClosingTime)
        {
            if (TimeManager.Instance.CurrentTime >= currentBuilding.buildingOpeningTime || TimeManager.Instance.CurrentTime < currentBuilding.buildingClosingTime)
            {
                return true;
            }

            return false;
        }
        else
        {
            if (TimeManager.Instance.CurrentTime >= currentBuilding.buildingOpeningTime && TimeManager.Instance.CurrentTime < currentBuilding.buildingClosingTime)
            {
                return true;
            }
                
            return false;
        }
    }
}
