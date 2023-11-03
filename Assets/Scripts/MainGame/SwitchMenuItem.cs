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
   [SerializeField] private Sprite[] images;
   [SerializeField] private Sprite cupNoodles;
   [SerializeField] private Sprite sandwich;
   [SerializeField] private Sprite siopao;
   [SerializeField] private Sprite hotdog;
   [SerializeField] private Sprite icedCoffee;
   [SerializeField] private Sprite icedMatcha;
   [SerializeField] private Sprite milkTea;
   [SerializeField] private Sprite fruitTea;
   [SerializeField] private Sprite slushie;
   [SerializeField] private Sprite smallMeal;
   [SerializeField] private Sprite mediumMeal;
   [SerializeField] private Sprite bigMeal;
   [SerializeField] private Sprite superMeal;
   [SerializeField] private Sprite eggRice;
   [SerializeField] private Sprite tofuRice;
   [SerializeField] private Sprite veggiesRice;
   [SerializeField] private Sprite chickenRice;
   [SerializeField] private Sprite porkRice;
   [SerializeField] private Sprite seafoodRice;

   private string[] foodList;
   private float[] priceList;
   private float[] energyBarIncrementValue;
   private float[] hungerBarIncrementValue;
   private float[] happinessBarIncrementValue;
   private float[] eatingTime;
   private string selectedBuilding = "CONVENIENCESTORE"; //Temporary variable
   private int currentItem = 0;

   public void Start()
   {
      AssignMenu();
      DisplayMenuText();
      DisplayImage();
   }  
   public void NextItem()
   {
      currentItem++;

      if (currentItem > foodList.Length - 1)
      {
         currentItem = 0;
      }

      DisplayMenuText();
      DisplayImage();
   }
   
   public void PreviousItem()
   {
      currentItem--;

      if (currentItem < 0)
      {
         currentItem = foodList.Length - 1;
      }

      DisplayMenuText();
      DisplayImage();
   }

   public void AssignMenu()
   {
      if (selectedBuilding == "CAFE")
      {
         foodList = new string[] {"Iced Latte and Bread", "Iced Matcha and Donut", "Milktea and Fries", "Fruit Tea and Clubhouse", "Slushie and Nachos"};
         priceList = new float[] {220, 260, 180, 190, 160};
         images = new Sprite[] {icedCoffee, icedMatcha, milkTea, fruitTea, slushie};
         energyBarIncrementValue = new float[] {15, 10, 0, 0, 0};
         hungerBarIncrementValue = new float[] {20, 20, 10, 10, 10};
         happinessBarIncrementValue = new float[] {25, 25, 15, 15, 10};
         eatingTime = new float[] {1.05f, 1.05f, 0.45f, 0.45f, 0.45f};
      }
      else if (selectedBuilding == "CONVENIENCESTORE")
      {
         foodList = new string[] {"Cup Noodles", "Sandwich", "Siopao", "Hotdog Sandwich"};
         priceList = new float[] {40, 38, 35, 32};
         images = new Sprite[] {cupNoodles, sandwich, siopao, hotdog};
         energyBarIncrementValue = new float[] {0, 0, 0, 0};
         hungerBarIncrementValue = new float[] {10, 10, 5, 5};
         happinessBarIncrementValue = new float[] {-10, -10, -10, -10, -10};
         eatingTime = new float[] {0.20f, 0.20f, 0.20f, 0.20f};
      }
      else if (selectedBuilding == "FOODXPRESS")
      {
         foodList = new string[] {"Small meal", "Medium meal", "Large meal", "Super meal"};
         priceList = new float[] {60, 90, 120, 150};
         images = new Sprite[] {smallMeal, mediumMeal, bigMeal, superMeal};
         energyBarIncrementValue = new float[] {2, 3, 5, 8};
         hungerBarIncrementValue = new float[] {20, 30, 35, 40};
         happinessBarIncrementValue = new float[] {3, 5, 10, 15};
         eatingTime = new float[] {0.25f, 0.30f, 0.30f, 1f};
      }
      else if (selectedBuilding == "CAFETERIA")
      {
         foodList = new string[] {"Egg and Rice", "Tofu and Rice", "Veggies and Rice", "Chicken and Rice", "Pork and Rice", "Seafood and Rice"};
         priceList = new float[] {30, 40, 40, 60, 60, 70};
         images = new Sprite[] {eggRice, tofuRice, veggiesRice, chickenRice, porkRice, seafoodRice};
         energyBarIncrementValue = new float[] {3, 5, 5, 8, 8, 8};
         hungerBarIncrementValue = new float[] {20, 30, 30, 35, 35, 40};
         happinessBarIncrementValue = new float[] {0, 3, 3, 5, 5, 8};
         eatingTime = new float[] {0.30f, 0.30f, 0.30f, 0.45f, 0.45f, 0.45f};
      }
   }

   public void DisplayMenuText()
   {
      foodName.text = foodList[currentItem];
      priceValue.text = "₱" + priceList[currentItem].ToString();
   }

   public void DisplayImage()
   {
      if (currentItem < images.Length && images[currentItem] != null) 
      {
         targetImage.sprite = images[currentItem];
      }
      else
      {
         Debug.LogWarning("Image not Found");
      }
   }

   public void ItemBought()
   {
      // FindObjectOfType<BuildingManager>().BuyFood(energyBarIncrementValue[currentItem], hungerBarIncrementValue[currentItem], happinessBarIncrementValue[currentItem], priceList[currentItem], eatingTime[currentItem]);
   }
}
