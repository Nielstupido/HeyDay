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
    [SerializeField] private Sprite[] images;
    [SerializeField] private Sprite scooterRed;
    [SerializeField] private Sprite scooterBlue;
    [SerializeField] private Sprite scooterBlack;
    [SerializeField] private Sprite scooterYellow;
    [SerializeField] private Sprite scooterCream;
    [SerializeField] private Sprite sedanRed;
    [SerializeField] private Sprite sedanBlue;
    [SerializeField] private Sprite sedanYellow;
    [SerializeField] private Sprite sedanBlack;
    [SerializeField] private Sprite suvRed;
    [SerializeField] private Sprite suvBlack;   
    [SerializeField] private Sprite suvBrown;
    [SerializeField] private Sprite suvBlue;
    [SerializeField] private Sprite coupeRed;
    [SerializeField] private Sprite coupeBlue;
    [SerializeField] private Sprite coupeBlack;
    [SerializeField] private Sprite coupeGrey;
    [SerializeField] private Sprite truckRed;
    [SerializeField] private Sprite truckBlue;
    [SerializeField] private Sprite truckCream;
    [SerializeField] private Sprite truckBlack;

    private string[] vehicleList;
    private float[] priceList;
    private string[] condition;
    private string[] color;
    private string[] type;
    private int currentItem = 0;


    public void BrandNewCatalogue()
    {
        buyCarPopUp.SetActive(false);
        carCatalogueOverlay.SetActive(true);

        vehicleList = new string[] {"SwiftRide Vroom Sporty", "SwiftRide Vroom 125", "SwiftRide Vroom 150", "NovaDrive Sedan (Manual)", "NovaDrive Sedan (Automatic)", 
        "VeloLux Coupe (Manual)", "VeloLux Coupe (Automatic)", "Zenith SUV (Manual)", "Zenith SUV (Automatic)", "Apex Pickup (Manual)",  "Apex Pickup (Automatic)"};
        color = new string[] {"Blue", "Red", "Black", "Red", "Black", "Black", "Red", "Brown", "Black", "Red", "Cream"};
        condition = new string[] {"Brand New", "Brand New", "Brand New", "Brand New", "Brand New", "Brand New", "Brand New", "Brand New", "Brand New", "Brand New", "Brand New"};
        type = new string[] {"Scooter", "Scooter", "Scooter", "Sedan", "Sedan", "Coupe", "Coupe", "SUV", "SUV", "Pickup", "Pickup"};
        priceList = new float[] {74900, 81000, 83500, 750000, 900000, 2000000, 2800000, 1500000, 2300000, 1600000, 1800000};
        images = new Sprite[] {scooterBlue, scooterRed, scooterBlack, sedanRed, sedanBlack, coupeBlack, coupeRed, suvBrown, suvBlack, truckRed, truckCream};

        DisplayText();
        DisplayImage();
    }

    public void SecondHandCatalogue()
    {
        buyCarPopUp.SetActive(false);
        carCatalogueOverlay.SetActive(true);

        vehicleList = new string[] {"SwiftRide Vroom Sporty", "SwiftRide Vroom 125", "NovaDrive Sedan (Manual)", "NovaDrive Sedan (Automatic)", 
        "VeloLux Coupe (Manual)", "VeloLux Coupe (Automatic)", "Zenith SUV (Manual)", "Zenith SUV (Automatic)", "Apex Pickup (Manual)",  "Apex Pickup (Automatic)"};
        color = new string[] {"Yellow", "Cream", "Yellow", "Blue", "Blue", "Grey", "Blue", "Red", "Blue", "Black"};
        condition = new string[] {"Heavily used", "Well used", "Heavily used", "Well used", "Heavily used", "Heavily used", "Well used", "Heavily used", "Well used", "Well used"};
        type = new string[] {"Scooter", "Scooter", "Sedan", "Sedan", "Coupe", "Coupe", "SUV", "SUV", "Pickup", "Pickup"};
        priceList = new float[] {15000, 20000, 90000, 120000, 300000, 400000, 250000, 350000, 280000, 370000};
        images = new Sprite[] {scooterYellow, scooterCream, sedanYellow, sedanBlue, coupeBlue, coupeGrey, suvBlue, suvRed, truckBlue, truckBlack};

        DisplayText();
        DisplayImage();
    }

    public void NextItem()
    {
        currentItem++;

        if (currentItem > vehicleList.Length - 1)
        {
            currentItem = 0;
        }

        DisplayText();
        DisplayImage();
    }

    public void PreviousItem()
    {
        currentItem--;

        if (currentItem < 0)
        {
            currentItem = vehicleList.Length - 1;
        }

        DisplayText();
        DisplayImage();
    }

    public void DisplayText()
    {
        vehicleName.text = vehicleList[currentItem];
        vehiclePrice.text = "₱ " + ConvertToCurrency(priceList[currentItem]);
        vehicleCondition.text = condition[currentItem];
        vehicleColor.text = color[currentItem];
        vehicleType.text = type[currentItem];
    }

    static string ConvertToCurrency(float amount)
    {
        return string.Format("{0:N0}", amount);
    }

    public void DisplayImage()
    {
        if (currentItem < images.Length && images[currentItem] != null) 
        {
            targetImage.sprite = images[currentItem];
        }
        else
        {
            Debug.LogWarning("Image not Found");
        }
    }

    public void ExitOverlay()
    {
        carCatalogueOverlay.SetActive(false);
        buyCarPopUp.SetActive(true);
    }

    public void BuyCar() //Passes the values to BuildingManager.Purchase
    {
        FindObjectOfType<BuildingManager>().Buy(priceList[currentItem]);
    }
}