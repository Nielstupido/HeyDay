using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mall : Building
{
    /*
    Services:

    Hair salon: +50 Happiness           Value: 400
    Barber shop: +50 Happiness          Value: 70
    Massage an Spa: +50 Happiness       Value: 600
    Gym: +50 Happiness                  Value: 500
    Dentist: +50 Happiness              Value: 1,000


    Appliances:

    Refrigerator: 
        Electric Bill = +50 pesos
        Happiness = +10
        Hunger = +30
        Value: 5,000
    Electric Stove: 
        Electric Bill = +40 pesos
        Hunger = +30    
        Value: 1,500             
    Microwave Oven:  
        Electric Bill = +30 pesos
        Hunger = +20
        Value: 3,000
    Water kettle:  
        Electric Bill = +10 pesos
        Hunger = +5
        Value: 200
    Air conditioner:  
        Electric Bill = +30 pesos
        Happiness = +10
        Value: 4,000
    Television:  
        Electric Bill = +30 pesos
        Happiness = +20
        Value: 3,000
    Desktop Computer:  
        Electric Bill = +20 pesos
        Happiness = +20
        Value: 40,000
    Laptop:  
        Electric Bill = +20 pesos
        Happiness = +20
        Value: 38,000
    Speaker:  
        Electric Bill = +10 pesos
        Happiness = +5
        Value: 1,000
    */

    [SerializeField] private GameObject exitServicesPanel;
    [SerializeField] private GameObject exitAppliancesPanel;
    [SerializeField] private GameObject servicesPanel;
    [SerializeField] private GameObject appliancesPanel;   
    [SerializeField] private GameObject mallChoosePanel;



    private void Start()
    {
        buildingName = Buildings.MALL;
        actionButtons = new List<Buttons>(){Buttons.APPLY, Buttons.QUIT};
        BuildingManager.Instance.onBuildingBtnClicked += CheckBtnClicked;
    }


    private void OnDestroy()
    {
        BuildingManager.Instance.onBuildingBtnClicked -= CheckBtnClicked;
    }


    public override void CheckBtnClicked(Buttons clickedBtn)
    {
        if (BuildingManager.Instance.CurrentSelectedBuilding.buildingName == this.buildingName)
            switch (clickedBtn)
            {
                case Buttons.DEPOSITMONEY:
                    Debug.Log("money deposited");
                    break;
                case Buttons.APPLY:
                    Debug.Log("money deposited");
                    break;
                case Buttons.WORK:
                    Debug.Log("money deposited");
                    break;
                case Buttons.QUIT:
                    Debug.Log("money deposited");
                    break;
            }
    }

    private void ShowServicesPanel()
    {
        servicesPanel.SetActive(true);
        appliancesPanel.SetActive(false);
    }

    // Show the appliances panel and hide the services panel
    private void ShowAppliancesPanel()
    {
        servicesPanel.SetActive(false);
        appliancesPanel.SetActive(true);
    }

        // Add this method to handle button clicks
    public void OnServicesBtnClicked()
    {
        ShowServicesPanel();
    }

    // Add this method to handle button clicks
    public void OnAppliancesBtnClicked()
    {
        ShowAppliancesPanel();
    }

    public void OnCloseBtnClicked()
    {
    servicesPanel.SetActive(false);
    appliancesPanel.SetActive(false);
    }


}