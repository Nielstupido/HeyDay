using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightClub : Building
{
    // Start is called before the first frame update
    void Start()
    {
        buildingName = Buildings.NIGHTCLUB;
        actionButtons = new List<Buttons>(){Buttons.APPLY, Buttons.QUIT};
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
