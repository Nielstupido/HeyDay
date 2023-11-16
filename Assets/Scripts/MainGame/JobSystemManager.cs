using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobSystemManager : MonoBehaviour
{
    [SerializeField] private JobPositions hospitalJobPositions;
    [SerializeField] private JobPositions cityHallJobPositions;
    [SerializeField] private JobPositions universityJobPositions;
    [SerializeField] private JobPositions fireStationJobPositions;
    [SerializeField] private JobPositions cafeteriaJobPositions;
    [SerializeField] private JobPositions policeStationJobPositions;
    [SerializeField] private JobPositions wasteMngmntFacilityJobPositions;
    [SerializeField] private JobPositions waterDistrictJobPositions;
    [SerializeField] private JobPositions bankJobPositions;
    [SerializeField] private JobPositions cafeJobPositions;
    [SerializeField] private JobPositions foodXpressJobPositions;
    [SerializeField] private JobPositions beeUsolutionsJobPositions;
    [SerializeField] private JobPositions autoDealerJobPositions;
    [SerializeField] private JobPositions convenienceStoreJobPositions;
    [SerializeField] private JobPositions factoryJobPositions;
    [SerializeField] private JobPositions callCenterJobPositions;
    [SerializeField] private JobPositions electricCompanyJobPositions;

    public static JobSystemManager Instance {private set; get;}


    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }


    public void ShowAvailablePositons()
    {

    }
}
