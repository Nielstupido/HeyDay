using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class University : Building
{
    [SerializeField] private GameObject universityOverlay;
    [SerializeField] private TextMeshProUGUI course;
    [SerializeField] private TextMeshProUGUI duration;

    [SerializeField] private GameObject fieldSelectionOverlay;
    [SerializeField] private GameObject courseSelectionOverlay;

    [SerializeField] private GameObject studyPromptOverlay;
    [SerializeField] private TMP_InputField studyDurationField;

    [SerializeField] private GameObject enrollPrompt;
    [SerializeField] private TextMeshProUGUI courseNamePrompt;

    [SerializeField] private GameObject studyAnimationOverlay;

    private string selectedCourse;
    private float selectedCourseDuration;

    public static University Instance { get; private set; }

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

    private void Start()
    {
        buildingName = Buildings.UNIVERSITY;
        actionButtons = new List<Buttons>(){Buttons.STUDY, Buttons.ENROL};
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
                case Buttons.STUDY:
                    studyPromptOverlay.SetActive(true);
                    break;
                case Buttons.ENROL:
                    fieldSelectionOverlay.SetActive(true);
                    break;
            }
    }

    public void ContinueStudy()
    {
        Player.Instance.Study(float.Parse(studyDurationField.text));
        studyPromptOverlay.SetActive(false);

        studyAnimationOverlay.SetActive(true);

        StartCoroutine(ClosePanelCoroutine(3));
    }

    public void CancelStudy()
    {
        studyPromptOverlay.SetActive(false);
    }

    public void Enroll(string courseName, float courseDuration)
    {
        selectedCourse = courseName;
        selectedCourseDuration = courseDuration;

        courseNamePrompt.text = selectedCourse;
        enrollPrompt.SetActive(true);
    }

    public void ContinueEnroll()
    {
        enrollPrompt.SetActive(false);
        courseSelectionOverlay.SetActive(false);
        universityOverlay.SetActive(true);
        course.text = selectedCourse;
        duration.text = "/" + selectedCourseDuration.ToString();

        Player.Instance.Enroll(selectedCourse, selectedCourseDuration);
    }

    public void CancelEnroll()
    {
        enrollPrompt.SetActive(false);
    }

    private IEnumerator ClosePanelCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        studyAnimationOverlay.SetActive(false);

    }
}
