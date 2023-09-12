using UnityEngine;
using System.Collections.Generic;

public abstract class Building : MonoBehaviour
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
    CARSHOP,
    CINEMA,
    FACTORY,
    FOODXPRESS,
    NIGHTCLUB,
    UNIVERSITY,
    BEEUSOLUTIONS,
    GOVERNMENTOFFICE
}


public enum Buttons
{
    BUYDRINK,
    BUYFOOD,
    APPLY,
    QUIT,
    WORK,
    DEPOSITMONEY,
    WATCHMOVIE,
    PARTY
}
