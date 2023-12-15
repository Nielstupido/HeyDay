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
    public GameObject ownedItemBtn;
    public Image itemImageHolder;
    public Button buyItem;
    private Items itemObj;


    private void Start()
    {
        buyItem.onClick.AddListener(BuyItem); // Attach the BuyItem method to the button click event
    }


    private void CheckItem()
    {
        if (Player.Instance.PlayerOwnedAppliances.Contains(itemObj))
        {
            // buyItem.image.sprite = itemOwnedBtn;
            ownedItemBtn.SetActive(true);
            buyItem.gameObject.SetActive(false);
        }
    }


    public void SetItemObj(Items thisItem)
    {
        itemObj = thisItem; 
        CheckItem();
    }


    private void BuyItem()
    {
        AudioManager.Instance.PlaySFX("Select");
        Player.Instance.Purchase(false, itemObj, 0.3f,5f);
    }
}
