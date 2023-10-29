using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResBuilding : MonoBehaviour
{
    [HideInInspector]public ResBuildings buildingName;
    [HideInInspector]public string buildingNameStr;
    [HideInInspector]public float monthlyRent;
    [HideInInspector]public float monthlyElecCharge;
    [HideInInspector]public float monthlyWaterCharge;
    [HideInInspector]public float dailyAdtnlHappiness;
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
