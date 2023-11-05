using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mall : Building
{
    [SerializeField] private GameObject shopOptionsOverlay;
    [SerializeField] private GameObject shopBrowserOverlay;
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private GameObject itemButtonParent;
    [SerializeField] private GameObject itemBoughtOverlay;
    [SerializeField] private GameObject purchaseErrorPrompt;
    [SerializeField] private Text header;
    [SerializeField] private Text itemName;
    [SerializeField] private Image itemImage;
    [SerializeField] private Sprite kettle;
    [SerializeField] private Sprite speakers;
    [SerializeField] private Sprite stove;
    [SerializeField] private Sprite microwave;
    [SerializeField] private Sprite tv;
    [SerializeField] private Sprite fridge;
    [SerializeField] private Sprite aircon;
    [SerializeField] private Sprite computer;
    [SerializeField] private Sprite laptop;
    [SerializeField] private Sprite barbershop;
    [SerializeField] private Sprite salon;
    [SerializeField] private Sprite gym;
    [SerializeField] private Sprite spa;
    [SerializeField] private Sprite dental;
    [SerializeField] private Sprite[] mallItemsImages;


    private string[] mallItems;
    private float[] mallItemsPrice;
    private float[] mallItemsPerks;

    private string optionSelected;
    public static Mall Instance { get; private set; }

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


    private void Start()
    {
        this.buildingName = Buildings.MALL;
        this.buildingOpeningTime = 10;
        this.buildingClosingTime = 22;

        actionButtons = new List<Buttons>(){Buttons.APPLY, Buttons.QUIT};
        BuildingManager.Instance.onBuildingBtnClicked += CheckBtnClicked;
    }


    private void OnDestroy()
    {
        BuildingManager.Instance.onBuildingBtnClicked -= CheckBtnClicked;
    }


    public override void CheckBtnClicked(Buttons clickedBtn)
    {
        if (BuildingManager.Instance.CurrentSelectedBuilding.buildingName == this.buildingName)
            switch (clickedBtn)
            {
                case Buttons.SHOP:
                    Debug.Log("money deposited");
                    break;
                case Buttons.APPLY:
                    Debug.Log("money deposited");
                    break;
                case Buttons.WORK:
                    Debug.Log("money deposited");
                    break;
                case Buttons.QUIT:
                    Debug.Log("money deposited");
                    break;
            }
    }

    public void ShopAppliances()
    {
        optionSelected = "Appliances";
        header.text = optionSelected;
        mallItems = new string[] {"Electric Water Kettle", "Speakers", "Electric Stove", "Microwave", "Flat Screen TV", "Refrigerator", "Air Conditioner", "Computer", "Laptop"};
        mallItemsPrice = new float[] {499, 699, 1499, 1999, 4999, 6499, 9499, 14599, 14699};
        mallItemsPerks = new float[] {5, 5, 10, 10, 10, 15, 15, 15, 15};
        mallItemsImages = new Sprite[] {kettle, speakers, stove, microwave, tv, fridge, aircon, computer, laptop};
        shopBrowserOverlay.SetActive(true);
        shopOptionsOverlay.SetActive(false);
        DisplayItems();
    }

    public void ShopServices()
    {
        optionSelected = "Services";
        header.text = optionSelected;
        mallItems = new string[] {"Barber Shop Service", "Hair Salon Service", "Gym Service", "Spa Service", "Dental Service"};
        mallItemsPrice = new float[] {150, 150, 150, 350, 500};
        mallItemsPerks = new float[] {20, 20, 20, 30, 50};
        mallItemsImages = new Sprite[] {barbershop, salon, gym, spa, dental};
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

        for (int i = 0; i < mallItems.Length; i++)
        {
            GameObject newButton = Instantiate(itemButtonPrefab, itemButtonParent.transform);
            ItemButton itemButton = newButton.GetComponent<ItemButton>();

            itemButton.itemName.text = mallItems[i];
            itemButton.itemPrice.text = "₱" + mallItemsPrice[i].ToString();
            itemButton.itemImageHolder.sprite = mallItemsImages[i];
            if (optionSelected == "Appliances")
            {
                string[] hungerAndElectricity = new string[] { "+5 Hunger, +₱20 Electric Bill",
                                                               "+₱20 Electric Bill", 
                                                               "+50 Hunger, +₱600 Electric Bill",
                                                               "+30 Hunger, +₱120 Electric Bill",
                                                               "+₱150 Electric Bill",
                                                               "+40 Hunger, +₱150 Electric Bill",
                                                               "+₱500 Electric Bill",
                                                               "+₱360 Electric Bill",
                                                               "+₱60 Electric Bill"};

                itemButton.itemPerks.text = "+" + mallItemsPerks[i].ToString() + " Happiness \n" + hungerAndElectricity[i];
            }
            else
            {
                itemButton.itemPerks.text = "+" + mallItemsPerks[i].ToString() + " Happiness";
            }
            itemButton.SetItemIndex(i);
        }
    }

    public void SelectItem(int itemIndex)
    {
        if (optionSelected == "Appliances")
        {
            if (!Player.Instance.itemsBought.Contains(itemIndex))
            {
                Player.Instance.PurchaseMallItem(5f, mallItemsPrice[itemIndex], mallItemsPerks[itemIndex]);
                Player.Instance.itemsBought.Add(itemIndex);
                
                DisplayItemBought(itemIndex);
            }
            else
            {
                purchaseErrorPrompt.SetActive(true);
                Debug.Log("Item already bought: " + mallItems[itemIndex]);
                StartCoroutine(ClosePrompt(0.8f));
            }
        }
        else
        {
            Player.Instance.PurchaseMallItem(5f, mallItemsPrice[itemIndex], mallItemsPerks[itemIndex]);
        }
    }
    
    public void DisplayItemBought(int i)
    {
        itemBoughtOverlay.SetActive(true);
        itemName.text = mallItems[i];
        itemImage.sprite = mallItemsImages[i];
    }

    private IEnumerator ClosePrompt(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        purchaseErrorPrompt.SetActive(false);

    }
}
