using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayonLuxeResidences : ResBuilding
{
    private void Start()
    {
        this.buildingName = ResBuildings.BAYONLUXERESIDENCES;
        this.buildingNameStr = "Bayon Luxe Residences";
        this.monthlyRent = 10000f;
        this.monthlyElecCharge = 1500f;
        this.monthlyWaterCharge = 500f;
        this.dailyAdtnlHappiness = 15f; 
    }
}
