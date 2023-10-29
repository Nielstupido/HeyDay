using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTPsuite : ResBuilding
{
    private void Start()
    {
        this.buildingName = ResBuildings.UTPSUITE;
        this.buildingNameStr = "UTP Suite";
        this.monthlyRent = 4500f;
        this.monthlyElecCharge = 800f;
        this.monthlyWaterCharge = 2500f;
        this.dailyAdtnlHappiness = 5f; 
    }
}
