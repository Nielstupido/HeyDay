using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : Building
{
    // Start is called before the first frame update
    void Start()
    {
        buildingName = Buildings.BANK;
        actionButtons = new List<Buttons>(){Buttons.DEPOSITMONEY, Buttons.APPLY, Buttons.WORK, Buttons.QUIT};
    }


    void Update()
    {
        
    }
}
