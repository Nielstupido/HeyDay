using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuildingSelect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject buildingSelectOverlay;
    [SerializeField] private Text buildingNameText;
    [SerializeField] private BuildingManager buildingManager;
    private bool isSelectBtnHidden = true;
    private Vector3 scaleDown = new Vector3(0.1f, 0.1f, 0.1f);
    

    private void Start()
    {
        buildingSelectOverlay.transform.localScale = scaleDown;
        buildingSelectOverlay.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelectBtnHidden)
        {
            buildingNameText.text = eventData.selectedObject.gameObject.name;
            buildingManager.CurrentSelectedBuilding = eventData.selectedObject.gameObject.name;
            buildingSelectOverlay.SetActive(true);
            AnimationManager.ScaleObj(buildingSelectOverlay, Vector3.one, 0.4f, false, LeanTweenType.easeOutBounce);
        }
        else
        {
            AnimationManager.ScaleObj(buildingSelectOverlay, scaleDown, 0.4f, true, LeanTweenType.easeInOutBack);
        }
        isSelectBtnHidden = !isSelectBtnHidden;
    }

}
