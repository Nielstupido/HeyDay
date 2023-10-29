using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeyDayTriangleResidences : ResBuilding
{
    private void Start()
    {
        this.buildingName = ResBuildings.HEYDAYTRIANGLERESIDENCES;
        this.buildingNameStr = "HeyDay Triangle Residences";
        this.monthlyRent = 7000f;
        this.monthlyElecCharge = 1100f;
        this.monthlyWaterCharge = 300f;
        this.dailyAdtnlHappiness = 10f; 
    }
}
