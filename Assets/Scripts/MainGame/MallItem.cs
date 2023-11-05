using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MallItem : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemHappinessPerks;
    public TextMeshProUGUI itemHungerBarPerks;
    public TextMeshProUGUI itemElectricBill;
    public TextMeshProUGUI itemPrice;
    public Image itemImageHolder;
    public Button buyItem;
    private int itemIndex;


    private void Start()
    {
        buyItem.onClick.AddListener(BuyItem); // Attach the BuyItem method to the button click event
    }


    public void SetItemIndex(int index)
    {
        itemIndex = index; // Set the index when creating the button
    }


    private void BuyItem()
    {
        MallManager.Instance.SelectItem(itemIndex); // Call the SelectItem method in ItemBrowser
    }
}
