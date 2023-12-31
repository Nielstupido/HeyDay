using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarCatalogueManager : MonoBehaviour
{
    [SerializeField] private GameObject carCatalogueOverlay;
    [SerializeField] private GameObject vehicleListOverlay;
    [SerializeField] private GameObject vehicleListPopUp;
    [SerializeField] private GameObject buyCarPopUp;
    [SerializeField] private Button buyBtn;
    [SerializeField] private TextMeshProUGUI vehicleName;
    [SerializeField] private TextMeshProUGUI vehiclePrice;
    [SerializeField] private TextMeshProUGUI vehicleColor;
    [SerializeField] private TextMeshProUGUI vehicleType;
    [SerializeField] private TextMeshProUGUI vehicleCondition;
    [SerializeField] private Image targetImage;
    [SerializeField] private List<Items> brandNewVehicles = new List<Items>();
    [SerializeField] private List<Items> secondHandVehicles = new List<Items>();
    public List<Items> BrandNewVehicles {get{return brandNewVehicles;}}
    public List<Items> SecondHandVehicles {get{return secondHandVehicles;}}
    private List<Items> vehicleDets = new List<Items>();
    private int currentItem = 0;
    public static CarCatalogueManager Instance { get; private set; }


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
    }


    private void Start()
    {
        buyBtn.onClick.AddListener( () => {BuyCar();} );
    }


    public void ShowCarCatalogue()
    {
        AudioManager.Instance.PlaySFX("Select");
        carCatalogueOverlay.SetActive(true);
    }

    public void ShowVehicleList()
    {
        vehicleListOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(vehicleListPopUp);
        buyCarPopUp.SetActive(false);
    }

    public void HideVehicleList()
    {
        AudioManager.Instance.PlaySFX("Select");
        vehicleListOverlay.SetActive(false);
        OverlayAnimations.Instance.AnimHideObj(vehicleListPopUp, vehicleListOverlay);
        buyCarPopUp.SetActive(true);
    }

    public void BrandNewCatalogue()
    {
        AudioManager.Instance.PlaySFX("Select");
        ShowVehicleList();

        vehicleDets = brandNewVehicles;
        currentItem = 0;

        DisplayItem();
    }


    public void SecondHandCatalogue()
    {
        AudioManager.Instance.PlaySFX("Select");
        ShowVehicleList();

        vehicleDets = secondHandVehicles;
        currentItem = 0;

        DisplayItem();
    }


    public void NextItem()
    {
        AudioManager.Instance.PlaySFX("Select");
        currentItem++;

        if (currentItem > vehicleDets.Count - 1)
        {
            currentItem = 0;
        }

        DisplayItem();
    }


    public void PreviousItem()
    {
        AudioManager.Instance.PlaySFX("Select");
        currentItem--;

        if (currentItem < 0)
        {
            currentItem = vehicleDets.Count - 1;
        }

        DisplayItem();
    }


    public void DisplayItem()
    {
        vehicleName.text = vehicleDets[currentItem].itemName;
        vehiclePrice.text = "₱ " + ConvertToCurrency(vehicleDets[currentItem].itemPrice + ((GameManager.Instance.InflationRate / 100) * vehicleDets[currentItem].itemPrice));
        vehicleCondition.text = vehicleDets[currentItem].itemCondition.ToString();
        vehicleColor.text = vehicleDets[currentItem].vehicleColor.ToString();
        vehicleType.text = vehicleDets[currentItem].vehicleType.ToString();
        targetImage.sprite = vehicleDets[currentItem].itemImage;
    }


    static string ConvertToCurrency(float amount)
    {
        return string.Format("{0:N0}", amount);
    }


    public void BuyCar() 
    {
        AudioManager.Instance.PlaySFX("Select");
        Player.Instance.Purchase(false, vehicleDets[currentItem], 1f);
    }
}