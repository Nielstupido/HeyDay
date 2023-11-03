using UnityEngine;


[CreateAssetMenu(menuName = "Game Items")]
public class Items : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite itemImage;
    public float itemPrice;
    public ItemCondition itemCondition;
    public VehicleType vehicleType;
    public VehicleColor vehicleColor;
    public float energyBarValue;
    public float hungerBarValue;
    public float happinessBarValue;
}