using UnityEngine;
using System.Collections.Generic;


public abstract class Building : MonoBehaviour
{
    [HideInInspector]public string buildingStringName;
    [HideInInspector]public Buildings buildingEnumName;
    [HideInInspector]public List<Buttons> actionButtons;
    [HideInInspector]public float buildingOpeningTime;
    [HideInInspector]public float buildingClosingTime;
    [HideInInspector]public float totalWorkHours;
    [HideInInspector]public bool currentlyHired;

    public abstract void CheckButtons();
    public abstract void CheckBtnClicked(Buttons clickedBtn);
}


public enum Buildings
{
    NONE,
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
    CITYHALL,
    FACTORY,
    CALLCENTER
}


public enum Buttons
{
    BUY,
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