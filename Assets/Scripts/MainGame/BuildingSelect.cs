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


    public void OnPointerClick(PointerEventData eventData)
    {
        if (BuildingManager.Instance.BuildingSelectOverlay.activeSelf && BuildingManager.Instance.CurrentSelectedBuilding == eventData.selectedObject.GetComponent<Building>())
        {
            AnimationManager.ScaleObj(BuildingManager.Instance.BuildingSelectOverlay, scaleDown, 0.4f, true, LeanTweenType.easeInOutBack);
        }
        else
        {
            BuildingManager.Instance.BuildingNameText = eventData.selectedObject.gameObject.name;
            BuildingManager.Instance.CurrentSelectedBuilding = eventData.selectedObject.GetComponent<Building>();
            Debug.Log("building name " + BuildingManager.Instance.CurrentSelectedBuilding);

            if (PlayerTravelManager.Instance.CurrentVisitedBuilding != null)
            {
                if (PlayerTravelManager.Instance.CurrentVisitedBuilding != BuildingManager.Instance.CurrentSelectedBuilding)
                {
                    BuildingManager.Instance.walkBtn.SetActive(true);
                    BuildingManager.Instance.rideBtn.SetActive(true);
                    BuildingManager.Instance.enterBtn.SetActive(false);
                }
                else
                {
                    BuildingManager.Instance.walkBtn.SetActive(false);
                    BuildingManager.Instance.rideBtn.SetActive(false);
                    BuildingManager.Instance.enterBtn.SetActive(true);
                }
            }

            if (!BuildingManager.Instance.BuildingSelectOverlay.activeSelf)
            {
                BuildingManager.Instance.BuildingSelectOverlay.SetActive(true);
                AnimationManager.ScaleObj(BuildingManager.Instance.BuildingSelectOverlay, Vector3.one, 0.4f, false, LeanTweenType.easeOutBounce);
            }
        }
    }

}
