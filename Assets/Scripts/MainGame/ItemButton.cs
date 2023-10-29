using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Text itemName;
    public Text itemPerks;
    public Text itemPrice;
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
        Mall.Instance.SelectItem(itemIndex); // Call the SelectItem method in ItemBrowser
    }
}
