using UnityEngine;
using System.Collections.Generic;

public class Building : MonoBehaviour
{
    [HideInInspector]public Buildings buildingName;
    [HideInInspector]public List<Buttons> actionButtons;


    public virtual void Work()
    {

    }


    public virtual void ApplyWork()
    {

    }


    public virtual void QuitWork()
    {
        
    }
}


public enum Buildings
{
    HOSPITAL,
    CAFE,
    BANK,
    MALL,
    CONVENIENCESTORE,
    CAFETERIA,
    CARSHOP,
    CINEMA,
    FACTORY,
    FOODXPRESS,
    NIGHTCLUB,
    UNIVERSITY,
    PRIVATEFIRM,
    GOVERNMENTOFFICE
}


public enum Buttons
{
    BUY,
    APPLY,
    QUIT,
    WORK,
    DEPOSITMONEY
}
