using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MallManager : MonoBehaviour
{
    [SerializeField] private GameObject mallOverlay;
    [SerializeField] private GameObject arcadeOverlay;
    [SerializeField] private GameObject slotMachine;
    [SerializeField] private GameObject shopOptionsOverlay;
    [SerializeField] private GameObject shopBrowserOverlay;
    [SerializeField] private GameObject shopBrowserPanel;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject itemsHolder;
    [SerializeField] private Image mallItemHeader;
    [SerializeField] private Sprite appliancesHeader;
    [SerializeField] private Sprite servicesHeader;
    [SerializeField] private List<Items> appliancesAvailable = new List<Items>();
    [SerializeField] private List<Items> servicesAvailable = new List<Items>();
    public List<Items> AppliancesAvailable {get{return appliancesAvailable;}}
    private List<Items> mallItems = new List<Items>();
    public static MallManager Instance { get; private set; }


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


    public void ShowMallOverlay()
    {
        AudioManager.Instance.PlaySFX("Select");
        mallOverlay.SetActive(true);
    }

    public void ExitMallOverlay()
    {
        AudioManager.Instance.PlaySFX("Select");
        mallOverlay.SetActive(false);
    }
    
    public void ShowMallItemBrowser()
    {
        AudioManager.Instance.PlaySFX("Select");
        shopBrowserPanel.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(shopBrowserOverlay);
        shopOptionsOverlay.SetActive(false);
        DisplayItems();
    }

    public void HideMallItemBrowser()
    {
        AudioManager.Instance.PlaySFX("Select");
        shopBrowserPanel.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(shopBrowserOverlay, shopBrowserPanel);
        shopOptionsOverlay.SetActive(true);
    }

    public void ShopAppliances()
    {
        mallItemHeader.sprite = appliancesHeader;
        mallItems = appliancesAvailable;
        ShowMallItemBrowser();
    }


    public void ShopServices()
    {
        mallItemHeader.sprite = servicesHeader;
        mallItems = servicesAvailable;
        ShowMallItemBrowser();
    }


    public void OpenArcade()
    {
        AudioManager.Instance.PlaySFX("Select");
        arcadeOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(slotMachine);
    }

    public void ExitArcade()
    {
        AudioManager.Instance.PlaySFX("Select");
        arcadeOverlay.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(slotMachine, arcadeOverlay);
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
            itemButton.itemPrice.text = "₱" + (mallItems[i].itemPrice + ((GameManager.Instance.InflationRate / 100) * mallItems[i].itemPrice)).ToString();
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
