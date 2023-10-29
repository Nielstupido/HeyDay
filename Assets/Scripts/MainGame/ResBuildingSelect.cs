using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ResBuildingSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ResBuildingManager resBuildingManager;
    private Vector3 scaleDown = new Vector3(0.1f, 0.1f, 0.1f);
    

    private void Start()
    {
        resBuildingManager.ResBuildingSelectOverlay.transform.localScale = scaleDown;
    }


    public void OnPointerClick(PointerEventData eventData)
    {

        if (resBuildingManager.ResBuildingSelectOverlay.activeSelf && resBuildingManager.CurrentSelectedResBuilding == eventData.selectedObject.GetComponent<ResBuilding>())
        {
            AnimationManager.ScaleObj(resBuildingManager.ResBuildingSelectOverlay, scaleDown, 0.4f, true, LeanTweenType.easeInOutBack);
        }
        else
        {
            Debug.Log(eventData);
            resBuildingManager.ResBuildingName = eventData.selectedObject.gameObject.GetComponent<ResBuilding>().buildingNameStr;
            resBuildingManager.MonthlyRentText = eventData.selectedObject.gameObject.GetComponent<ResBuilding>().monthlyRent.ToString();
            resBuildingManager.MonthlyWaterText = eventData.selectedObject.gameObject.GetComponent<ResBuilding>().monthlyWaterCharge.ToString();
            resBuildingManager.MonthlyElecText = eventData.selectedObject.gameObject.GetComponent<ResBuilding>().monthlyElecCharge.ToString();
            resBuildingManager.DailyHappinessAdtnlText = eventData.selectedObject.gameObject.GetComponent<ResBuilding>().dailyAdtnlHappiness.ToString();
            resBuildingManager.CurrentSelectedResBuilding = eventData.selectedObject.GetComponent<ResBuilding>();
            Debug.Log("building name " + resBuildingManager.CurrentSelectedResBuilding);

            if (!resBuildingManager.ResBuildingSelectOverlay.activeSelf)
            {
                resBuildingManager.ResBuildingSelectOverlay.SetActive(true);
                AnimationManager.ScaleObj(resBuildingManager.ResBuildingSelectOverlay, Vector3.one, 0.4f, false, LeanTweenType.easeOutBounce);
            }
        }
    }

}