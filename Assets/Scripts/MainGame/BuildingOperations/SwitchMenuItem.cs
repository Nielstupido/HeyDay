using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchMenuItem : MonoBehaviour
{
   [SerializeField] private GameObject menuOverlay;
   [SerializeField] private GameObject menuPopUp;
   [SerializeField] private TextMeshProUGUI foodName;
   [SerializeField] private TextMeshProUGUI priceValue;
   [SerializeField] private Image targetImage;
   [SerializeField] private List<Items> cafeFoodList = new List<Items>();
   [SerializeField] private List<Items> cafeteriaFoodList = new List<Items>();
   [SerializeField] private List<Items> convenienceStoreFoodList = new List<Items>();
   [SerializeField] private List<Items> foodxPressFoodList = new List<Items>();
   private List<Items> foodList = new List<Items>();
   private int currentItem = 0;


   public void ShowConsumablesMenu()
   {
      //AudioManager.Instance.PlaySFX("Select");
      menuOverlay.SetActive(true);
      OverlayAnimations.Instance.AnimOpenOverlay(menuPopUp);
      
      AssignMenu();
      DisplayItem();
   }

   public void HideConsumablesMenu()
   {
      AudioManager.Instance.PlaySFX("Select");
      OverlayAnimations.Instance.AnimCloseOverlay(menuPopUp, menuOverlay);
      menuOverlay.SetActive(false);
   }  


   public void NextItem()
   {
      AudioManager.Instance.PlaySFX("Select");
      currentItem++;

      if (currentItem > foodList.Count - 1)
      {
         currentItem = 0;
      }

      DisplayItem();
   }

   
   public void PreviousItem()
   {
      AudioManager.Instance.PlaySFX("Select");
      currentItem--;

      if (currentItem < 0)
      {
         currentItem = foodList.Count - 1;
      }

      DisplayItem();
   }


   private void AssignMenu()
   {
      currentItem = 0;

      switch (BuildingManager.Instance.CurrentSelectedBuilding.buildingEnumName)
      {
         case Buildings.CAFE:
            foodList = cafeFoodList;
            break;
         case Buildings.CONVENIENCESTORE:
            foodList = convenienceStoreFoodList;
            break;
         case Buildings.FOODXPRESS:
            foodList = foodxPressFoodList;
            break;
         case Buildings.CAFETERIA:
            foodList = cafeteriaFoodList;
            break;
      }
   }


   private void DisplayItem()
   {
      foodName.text = foodList[currentItem].itemName;
      priceValue.text = "₱" + (foodList[currentItem].itemPrice + ((GameManager.Instance.InflationRate / 100) * foodList[currentItem].itemPrice)).ToString();
      targetImage.sprite = foodList[currentItem].itemImage;
   }


   public void BuyItem()
   {
      AudioManager.Instance.PlaySFX("Select");
      Player.Instance.Purchase(true, foodList[currentItem], 0.3f);
   }
}
