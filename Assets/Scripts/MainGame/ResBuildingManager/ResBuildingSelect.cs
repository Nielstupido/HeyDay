using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ResBuildingSelect : MonoBehaviour, IPointerClickHandler
{
    private Vector3 scaleDown = new Vector3(0.1f, 0.1f, 0.1f);
    

    private void Start()
    {
        ResBuildingManager.Instance.ResBuildingSelectOverlay.transform.localScale = scaleDown;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX("Select");
        if (ResBuildingManager.Instance.ResBuildingSelectOverlay.activeSelf && ResBuildingManager.Instance.CurrentSelectedResBuilding == eventData.selectedObject.GetComponent<ResBuilding>())
        {
            AnimationManager.ScaleObj(ResBuildingManager.Instance.ResBuildingSelectOverlay, scaleDown, 0.4f, true, LeanTweenType.easeInOutBack);
        }
        else
        {
            ResBuildingManager.Instance.ResBuildingName = eventData.selectedObject.gameObject.GetComponent<ResBuilding>().buildingNameStr;
            ResBuildingManager.Instance.MonthlyRentText = eventData.selectedObject.gameObject.GetComponent<ResBuilding>().monthlyRent.ToString();
            ResBuildingManager.Instance.MonthlyWaterText = eventData.selectedObject.gameObject.GetComponent<ResBuilding>().monthlyWaterCharge.ToString();
            ResBuildingManager.Instance.MonthlyElecText = eventData.selectedObject.gameObject.GetComponent<ResBuilding>().monthlyElecCharge.ToString();
            ResBuildingManager.Instance.DailyHappinessAdtnlText = eventData.selectedObject.gameObject.GetComponent<ResBuilding>().dailyAdtnlHappiness.ToString();
            ResBuildingManager.Instance.CurrentSelectedResBuilding = eventData.selectedObject.GetComponent<ResBuilding>();

            if (!ResBuildingManager.Instance.ResBuildingSelectOverlay.activeSelf)
            {
                ResBuildingManager.Instance.ResBuildingSelectOverlay.SetActive(true);
                AnimationManager.ScaleObj(ResBuildingManager.Instance.ResBuildingSelectOverlay, Vector3.one, 0.4f, false, LeanTweenType.easeOutBounce);
            }
        }
    }

}