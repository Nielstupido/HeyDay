using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class University : Building
{
    private void Start()
    {
        this.buildingStringName = "Heyday University";
        this.buildingEnumName = Buildings.UNIVERSITY;
        this.buildingOpeningTime = 7f;
        this.buildingClosingTime = 17f;
        this.buildingDescription = "Welcome to HeyDay University, where knowledge meets opportunity. Sprawling across manicured lawns and modern architecture, the campus buzzes with scholarly pursuits. Towering lecture halls, bustling libraries, and lively student hangouts define the vibrant academic atmosphere.";

        BuildingManager.Instance.onBuildingBtnClicked += CheckBtnClicked;
        GameManager.Instance.MeetupLocBuildings.Add(this);
    }


    private void OnDestroy()
    {
        BuildingManager.Instance.onBuildingBtnClicked -= CheckBtnClicked;
    }


    public override void CheckBtnClicked(Buttons clickedBtn)
    {
        if (BuildingManager.Instance.CurrentSelectedBuilding.buildingEnumName == this.buildingEnumName)
            switch (clickedBtn)
            {
                case Buttons.APPLY:
                    JobManager.Instance.Apply(this);
                    break;
                case Buttons.WORK:
                    JobManager.Instance.Work();
                    break;
                case Buttons.QUIT:
                    JobManager.Instance.QuitWork();
                    break;
                case Buttons.STUDY:
                    BuildingManager.Instance.OpenUniversityStudyOverlay();
                    break;
                case Buttons.ENROL:
                    BuildingManager.Instance.OpenUniversityEnrollOverlay();
                    break;
            }
    }


    public override void CheckButtons()
    {
        UniversityManager.Instance.OnEnteredUniversity();
        this.actionButtons = new List<Buttons>(){Buttons.APPLY};

        if (GameManager.Instance.CurrentGameLevel == 1)
        {
            this.actionButtons.Add(Buttons.ENROL);
        }

        if (Player.Instance.PlayerEnrolledCourse != UniversityCourses.NONE)
        {
            this.actionButtons.Add(Buttons.STUDY);
        }

        if (this.currentlyHired && this.buildingEnumName == Player.Instance.CurrentPlayerJob.establishment)
        {
            this.actionButtons.Add(Buttons.WORK);
            this.actionButtons.Add(Buttons.QUIT);  
        }
    }
}
