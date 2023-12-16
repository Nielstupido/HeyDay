using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerItemsListManager : MonoBehaviour
{
    [SerializeField] private GameObject itemsHolderOverlay;
    [SerializeField] private GameObject itemsHolderPopUp;
    [SerializeField] private Transform itemsContentHolder;
    [SerializeField] private GameObject itemsObjPrefab;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Image overlayBg;
    [SerializeField] private Sprite livingRoomBg;
    [SerializeField] private Sprite garageBg;



    public void ShowItems(ItemType itemType, List<Items> playerItems)
    {
        AudioManager.Instance.PlaySFX("Select");
        HideItems();
        itemsHolderOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(itemsHolderPopUp);

        switch (itemType)
        {
            case ItemType.VEHICLE:
                titleText.text = "Vehicles Owned";
                overlayBg.sprite = garageBg;
                break;
            case ItemType.APPLIANCE:
                titleText.text = "Appliances Owned";
                overlayBg.sprite = livingRoomBg;
                break;
        }

        foreach (Items playerItem in playerItems)
        {
            GameObject newItem = Instantiate(itemsObjPrefab, Vector3.zero, Quaternion.identity, itemsContentHolder);
            newItem.GetComponent<ItemsObjPrefab>().ItemImageSprite = playerItem.itemImage;
        }
    }

    public void HidePlayerItems()
    {
        AudioManager.Instance.PlaySFX("Select");
        OverlayAnimations.Instance.AnimCloseOverlay(itemsHolderPopUp, itemsHolderOverlay);
    }

    public void HideItems()
    {
        for (var i = itemsContentHolder.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(itemsContentHolder.GetChild(i).gameObject);
        }
    }
}
