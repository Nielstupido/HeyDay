using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarCatalogueManager : MonoBehaviour
{
    [SerializeField] private GameObject carCatalogueOverlay;
    [SerializeField] private GameObject buyCarPopUp;
    [SerializeField] private TextMeshProUGUI vehicleName;
    [SerializeField] private TextMeshProUGUI vehiclePrice;
    [SerializeField] private TextMeshProUGUI vehicleColor;
    [SerializeField] private TextMeshProUGUI vehicleType;
    [SerializeField] private TextMeshProUGUI vehicleCondition;
    [SerializeField] private Image targetImage;
    [SerializeField] private List<Items> brandNewVehicles = new List<Items>();
    [SerializeField] private List<Items> secondHandVehicles = new List<Items>();
    private List<Items> vehicleDets = new List<Items>();
    private int currentItem = 0;


    public void BrandNewCatalogue()
    {
        buyCarPopUp.SetActive(false);
        carCatalogueOverlay.SetActive(true);

        vehicleDets = brandNewVehicles;
        currentItem = 0;

        DisplayItem();
    }


    public void SecondHandCatalogue()
    {
        buyCarPopUp.SetActive(false);
        carCatalogueOverlay.SetActive(true);

        vehicleDets = brandNewVehicles;
        currentItem = 0;

        DisplayItem();
    }


    public void NextItem()
    {
        currentItem++;

        if (currentItem > vehicleDets.Count - 1)
        {
            currentItem = 0;
        }

        DisplayItem();
    }


    public void PreviousItem()
    {
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
        vehiclePrice.text = "₱ " + ConvertToCurrency(vehicleDets[currentItem].itemPrice);
        vehicleCondition.text = vehicleDets[currentItem].itemCondition.ToString();
        vehicleColor.text = vehicleDets[currentItem].vehicleColor.ToString();
        vehicleType.text = vehicleDets[currentItem].vehicleType.ToString();
        targetImage.sprite = vehicleDets[currentItem].itemImage;
    }


    static string ConvertToCurrency(float amount)
    {
        return string.Format("{0:N0}", amount);
    }


    public void ExitOverlay()
    {
        carCatalogueOverlay.SetActive(false);
        buyCarPopUp.SetActive(true);
    }


    public void BuyCar() //Passes the values to BuildingManager.Purchase
    {
        FindObjectOfType<BuildingManager>().Buy(vehicleDets[currentItem]);
    }
}