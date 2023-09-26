using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject bottomOverlay;
    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private GameObject buildingInteriorOverlay;
    public static GameManager Instance { get; private set; }


    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }


    public void StartGame()
    {
        bottomOverlay.SetActive(true);
        pauseBtn.SetActive(true);
        AudioManager.Instance.PlayMusic("Theme");
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
