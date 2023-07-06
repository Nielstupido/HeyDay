using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject bottomOverlay;
    [SerializeField] private GameObject buildingInteriorOverlay;


    public void StartGame()
    {
        bottomOverlay.SetActive(true);
    }


    public void EnterBuilding()
    {
        buildingInteriorOverlay.SetActive(true);
    }


    public void ExitBuilding()
    {
        buildingInteriorOverlay.SetActive(false);
    }
}
