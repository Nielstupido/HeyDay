using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cafe : Building
{
    void Start()
    {
        buildingName = Buildings.CAFE;
        actionButtons = new List<Buttons>(){Buttons.BUY, Buttons.QUIT};
    }

    void Update()
    {
        
    }
}
