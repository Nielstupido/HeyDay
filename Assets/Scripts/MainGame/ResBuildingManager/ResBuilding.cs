using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResBuilding : MonoBehaviour
{
    [HideInInspector]public ResBuildings buildingEnumName;
    [HideInInspector]public List<Buttons> actionButtons;
    [HideInInspector]public string buildingNameStr;
    [HideInInspector]public float monthlyRent;
    [HideInInspector]public float monthlyElecCharge;
    [HideInInspector]public float monthlyWaterCharge;
    [HideInInspector]public float dailyAdtnlHappiness;
    [HideInInspector]public float adtnlEnergyForSleep;


    public virtual void Sleep()
    {

    }


    public virtual void Eat()
    {

    }


    public abstract void CheckBtnClicked(Buttons clickedBtn);
}



public enum ResBuildings
{
    NONE,
    BAYONLUXERESIDENCES,
    HEYDAYTRIANGLERESIDENCES,
    REGALSANCTUARY,
    SAMPAGUITACONDOTEL,
    UTPSUITE
}
