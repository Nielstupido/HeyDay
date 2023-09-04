using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateFirm : Building
{
    // Start is called before the first frame update
    void Start()
    {
        buildingName = Buildings.BANK;
        actionButtons = new List<Buttons>(){Buttons.WORK, Buttons.QUIT};
    }


    void Update()
    {
        
    }
}
