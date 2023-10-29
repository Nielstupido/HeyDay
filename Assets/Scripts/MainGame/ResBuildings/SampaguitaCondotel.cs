using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampaguitaCondotel : ResBuilding
{
    private void Start()
    {
        this.buildingName = ResBuildings.SAMPAGUITACONDOTEL;
        this.buildingNameStr = "Sampaguita Condotel";
        this.monthlyRent = 10500f;
        this.monthlyElecCharge = 1500f;
        this.monthlyWaterCharge = 500f;
        this.dailyAdtnlHappiness = 17f; 
    }
}
