using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchMenuItem : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI foodName;
   [SerializeField] private TextMeshProUGUI priceValue;
   [SerializeField] private Image targetImage;
   [SerializeField] private List<Items> cafeFoodList = new List<Items>();
   [SerializeField] private List<Items> cafeteriaFoodList = new List<Items>();
   [SerializeField] private List<Items> convenienceStoreFoodList = new List<Items>();
   [SerializeField] private List<Items> foodxPressFoodList = new List<Items>();
   private List<Items> foodList = new List<Items>();
   private Buildings selectedBuilding = Buildings.CAFE; //temporary
   private int currentItem = 0;


   public void Start()
   {
      AssignMenu();
      DisplayItem();
   }  


   public void NextItem()
   {
      currentItem++;

      if (currentItem > foodList.Count - 1)
      {
         currentItem = 0;
      }

      DisplayItem();
   }

   
   public void PreviousItem()
   {
      currentItem--;

      if (currentItem < 0)
      {
         currentItem = foodList.Count - 1;
      }

      DisplayItem();
   }


   public void AssignMenu()
   {
      currentItem = 0;

      switch (selectedBuilding)
      // switch (BuildingManager.Instance.CurrentSelectedBuilding.buildingName)
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


   public void DisplayItem()
   {
      foodName.text = foodList[currentItem].itemName;
      priceValue.text = "₱" + foodList[currentItem].itemPrice.ToString();
      targetImage.sprite = foodList[currentItem].itemImage;
   }


   public void ItemBought()
   {
      Player.Instance.Purchase(true, foodList[currentItem]);
   }
}
