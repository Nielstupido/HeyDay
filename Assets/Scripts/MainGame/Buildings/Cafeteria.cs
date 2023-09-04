using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cafeteria : Building
{
    void Start()
    {
        buildingName = Buildings.CAFETERIA;
        actionButtons = new List<Buttons>(){Buttons.APPLY, Buttons.QUIT};
    }

    void Update()
    {
        
    }
}
