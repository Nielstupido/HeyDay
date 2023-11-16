using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuildingSelect : MonoBehaviour, IPointerClickHandler
{
    private Vector3 scaleDown = new Vector3(0.1f, 0.1f, 0.1f);


    private void Start()
    {
        BuildingManager.Instance.BuildingSelectOverlay.transform.localScale = scaleDown;
    }
    

    public void RefreshSelectOverlayUI()
    {
        if (PlayerTravelManager.Instance.CurrentVisitedBuilding != null)
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
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (BuildingManager.Instance.BuildingSelectOverlay.activeSelf && BuildingManager.Instance.CurrentSelectedBuilding == eventData.selectedObject.GetComponent<Building>())
        {
            AnimationManager.ScaleObj(BuildingManager.Instance.BuildingSelectOverlay, scaleDown, 0.4f, true, LeanTweenType.easeInOutBack);
        }
        else
        {
            Building currentSelectedBuilding = eventData.selectedObject.GetComponent<Building>();
            BuildingManager.Instance.BuildingNameText = currentSelectedBuilding.buildingStringName;
            BuildingManager.Instance.CurrentSelectedBuilding = currentSelectedBuilding;
            RefreshSelectOverlayUI();

            if (!BuildingManager.Instance.BuildingSelectOverlay.activeSelf)
            {
                BuildingManager.Instance.BuildingSelectOverlay.SetActive(true);
                AnimationManager.ScaleObj(BuildingManager.Instance.BuildingSelectOverlay, Vector3.one, 0.4f, false, LeanTweenType.easeOutBounce);
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
            if (TimeManager.Instance.CurrentTime > currentBuilding.buildingOpeningTime || TimeManager.Instance.CurrentTime < currentBuilding.buildingClosingTime)
            {
                return true;
            }

            return false;
        }
        else
        {
            if (TimeManager.Instance.CurrentTime > currentBuilding.buildingOpeningTime && TimeManager.Instance.CurrentTime < currentBuilding.buildingClosingTime)
            {
                return true;
            }
                
            return false;
        }
    }
}
