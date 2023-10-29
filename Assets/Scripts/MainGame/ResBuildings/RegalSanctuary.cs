using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegalSanctuary : ResBuilding
{
    private void Start()
    {
        this.buildingName = ResBuildings.REGALSANCTUARY;
        this.buildingNameStr = "Regal Sanctuary";
        this.monthlyRent = 6000f;
        this.monthlyElecCharge = 1000f;
        this.monthlyWaterCharge = 250f;
        this.dailyAdtnlHappiness = 8f; 
    }
}
