using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MallManager : MonoBehaviour
{
    [SerializeField] private GameObject shopOptionsOverlay;
    [SerializeField] private GameObject shopBrowserOverlay;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject itemsHolder;
    [SerializeField] private GameObject itemBoughtOverlay;
    [SerializeField] private Image mallItemHeader;
    [SerializeField] private Sprite appliancesHeader;
    [SerializeField] private Sprite servicesHeader;
    [SerializeField] private List<Items> appliancesAvailable = new List<Items>();
    [SerializeField] private List<Items> servicesAvailable = new List<Items>();

    private List<Items> mallItems = new List<Items>();
    public static MallManager Instance { get; private set; }


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
    

    public void ShopAppliances()
    {
        mallItemHeader.sprite = appliancesHeader;
        mallItems = appliancesAvailable;
        // mallItems = new string[] {"Electric Water Kettle", "Speakers", "Electric Stove", "Microwave", "Flat Screen TV", "Refrigerator", "Air Conditioner", "Computer", "Laptop"};
        // mallItemsPrice = new float[] {499, 699, 1499, 1999, 4999, 6499, 9499, 14599, 14699};
        // mallItemsPerks = new float[] {5, 5, 10, 10, 10, 15, 15, 15, 15};
        // mallItemsImages = new Sprite[] {kettle, speakers, stove, microwave, tv, fridge, aircon, computer, laptop};
        shopBrowserOverlay.SetActive(true);
        shopOptionsOverlay.SetActive(false);
        DisplayItems();
    }


    public void ShopServices()
    {
        mallItemHeader.sprite = servicesHeader;
        mallItems = servicesAvailable;
        // mallItems = new string[] {"Barber Shop Service", "Hair Salon Service", "Gym Service", "Spa Service", "Dental Service"};
        // mallItemsPrice = new float[] {150, 150, 150, 350, 500};
        // mallItemsPerks = new float[] {20, 20, 20, 30, 50};
        // mallItemsImages = new Sprite[] {barbershop, salon, gym, spa, dental};
        shopBrowserOverlay.SetActive(true);
        shopOptionsOverlay.SetActive(false);
        DisplayItems();
    }


    private void DisplayItems()
    {
        for (int i = 0; i < itemsHolder.transform.childCount; i++)
        {
            Destroy(itemsHolder.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < mallItems.Count; i++)
        {
            GameObject newButton = Instantiate(itemPrefab, itemsHolder.transform);
            MallItem itemButton = newButton.GetComponent<MallItem>();

            itemButton.SetItemObj(mallItems[i]);
            itemButton.itemName.text = mallItems[i].itemName;
            itemButton.itemPrice.text = "₱" + mallItems[i].itemPrice.ToString();
            itemButton.itemImageHolder.sprite = mallItems[i].itemImage;
            itemButton.itemHungerBarPerks.text = "+ " + mallItems[i].hungerBarValue.ToString() + " Hunger";
            itemButton.itemHappinessPerks.text = "+ " + mallItems[i].happinessBarValue.ToString() + " Happiness";
            itemButton.itemElectricBill.text = "Electric Bill: ₱ " + mallItems[i].electricBillValue.ToString();

            if (mallItems[i].itemType == ItemType.SERVICE)
            {
                itemButton.itemElectricBill.text = "";
            }
        }
    }
}
