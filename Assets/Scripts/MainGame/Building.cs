using UnityEngine;
using System.Collections.Generic;


public abstract class Building : MonoBehaviour
{
    [HideInInspector]public Buildings buildingName;
    [HideInInspector]public List<Buttons> actionButtons;
    [HideInInspector]public int buildingOpeningTime;
    [HideInInspector]public int buildingClosingTime;


    public abstract void CheckBtnClicked(Buttons clickedBtn);
}


public enum Buildings
{
    HOSPITAL,
    CAFE,
    BANK,
    MALL,
    CONVENIENCESTORE,
    CAFETERIA,
    AUTODEALER,
    CINEMA,
    FOODXPRESS,
    NIGHTCLUB,
    UNIVERSITY,
    BEEUSOLUTIONS,
    POLICESTATION,
    RESIDENTIAL,
    WATERDISTRICT,
    ELECTRICCOOP,
    WASTEFACILITY,
    FIRESTATION,
    CITYHALL
}


public enum Buttons
{
    BUYDRINK,
    BUYFOOD,
    APPLY,
    QUIT,
    WORK,
    OPENSAVINGSACCOUNT,
    ACCESSSAVINGSACCOUNT,
    WATCHMOVIE,
    PARTY,
    STUDY,
    ENROL,
    ENTER,
    SHOP,
    SLEEP,
    EAT
}
