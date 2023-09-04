using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuildingSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Text buildingNameText;
    [SerializeField] private BuildingManager buildingManager;
    private Vector3 scaleDown = new Vector3(0.1f, 0.1f, 0.1f);
    

    private void Start()
    {
        buildingManager.BuildingSelectOverlay.transform.localScale = scaleDown;
    }


    public void OnPointerClick(PointerEventData eventData)
    {

        if (buildingManager.BuildingSelectOverlay.activeSelf && buildingManager.CurrentSelectedBuilding == eventData.selectedObject.GetComponent<Building>())
        {
            AnimationManager.ScaleObj(buildingManager.BuildingSelectOverlay, scaleDown, 0.4f, true, LeanTweenType.easeInOutBack);
        }
        else
        {
            buildingNameText.text = eventData.selectedObject.gameObject.name;
            buildingManager.CurrentSelectedBuilding = eventData.selectedObject.GetComponent<Building>();
            Debug.Log("building name " + buildingManager.CurrentSelectedBuilding);

            if (!buildingManager.BuildingSelectOverlay.activeSelf)
            {
                buildingManager.BuildingSelectOverlay.SetActive(true);
                AnimationManager.ScaleObj(buildingManager.BuildingSelectOverlay, Vector3.one, 0.4f, false, LeanTweenType.easeOutBounce);
            }
        }
    }

}
