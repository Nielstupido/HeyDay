using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MallManager : MonoBehaviour
{
    [SerializeField] private GameObject shopOptionsOverlay;
    [SerializeField] private GameObject shopBrowserOverlay;
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private GameObject itemButtonParent;
    [SerializeField] private GameObject itemBoughtOverlay;
    [SerializeField] private Text header;
    [SerializeField] private Text itemName;
    [SerializeField] private Image itemImage;
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
        header.text = appliancesAvailable[0].itemType.ToString();
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
        header.text = servicesAvailable[0].itemType.ToString();
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
        foreach (Transform child in itemButtonParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < mallItems.Count; i++)
        {
            GameObject newButton = Instantiate(itemButtonPrefab, itemButtonParent.transform);
            MallItem itemButton = newButton.GetComponent<MallItem>();

            itemButton.itemName.text = mallItems[i].itemName;
            itemButton.itemPrice.text = "₱" + mallItems[i].itemPrice.ToString();
            itemButton.itemImageHolder.sprite = mallItems[i].itemImage;
            itemButton.itemHungerBarPerks.text = "+ " + mallItems[i].hungerBarValue.ToString() + " Hunger";
            itemButton.itemHappinessPerks.text = "+ " + mallItems[i].happinessBarValue.ToString() + " Happiness";
            itemButton.itemElectricBill.text = "₱ " + mallItems[i].electricBillValue.ToString();

            if (mallItems[i].itemType == ItemType.SERVICE)
            {
                itemButton.itemElectricBill.text = "";
            }
            itemButton.SetItemIndex(i);
        }
    }


    public void SelectItem(int itemIndex)
    {
        // if (optionSelected == "Appliances")
        // {
        //     if (!Player.Instance.itemsBought.Contains(itemIndex))
        //     {
        //         Player.Instance.PurchaseMallItem(5f, mallItemsPrice[itemIndex], mallItemsPerks[itemIndex]);
        //         Player.Instance.itemsBought.Add(itemIndex);
                
        //         DisplayItemBought(itemIndex);
        //     }
        //     else
        //     {
        //         Debug.Log("Item already bought: " + mallItems[itemIndex]);
        //     }
        // }
        // else
        // {
        //     Player.Instance.PurchaseMallItem(5f, mallItemsPrice[itemIndex], mallItemsPerks[itemIndex]);
        // }
    }
    

    public void DisplayItemBought(int i)
    {
        // itemBoughtOverlay.SetActive(true);
        // itemName.text = mallItems[i];
        // itemImage.sprite = mallItemsImages[i];
    }
}
