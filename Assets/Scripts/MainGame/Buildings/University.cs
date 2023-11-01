using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class University : Building
{
    [SerializeField] private GameObject universityOverlay;
    [SerializeField] private TextMeshProUGUI course;
    [SerializeField] private TextMeshProUGUI duration;
    [SerializeField] private TextMeshProUGUI totalStudyHours;

    [SerializeField] private GameObject fieldSelectionOverlay;
    [SerializeField] private TextMeshProUGUI fieldNameText; 
    [SerializeField] private GameObject courseSelectionOverlay;
    [SerializeField] private GameObject courseButtonPrefab;
    [SerializeField] private GameObject courseButtonParent;

    [SerializeField] private GameObject studyPromptOverlay;
    [SerializeField] private TMP_InputField studyDurationField;

    [SerializeField] private GameObject enrollPrompt;
    [SerializeField] private TextMeshProUGUI courseNamePrompt;

    [SerializeField] private GameObject studyAnimationOverlay;

    private string selectedField;
    private string selectedCourse;
    private float selectedCourseDuration;
    private string[] courses;
    private float[] courseDuration;

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
        this.buildingName = Buildings.UNIVERSITY;
        this.buildingOpeningTime = 7;
        this.buildingClosingTime = 17;

        this.actionButtons = new List<Buttons>(){Buttons.STUDY, Buttons.ENROL};
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

    public void ArchitectureAndDesign()
    {
        selectedField = "Architecture and Design";
        fieldNameText.text = selectedField;

        courses = new string[] {"Bachelor of Science in Architecture", 
                                "Bachelor of Science in Interior Design",
                                "Bachelor of Science in Industrial Design"};
        courseDuration = new float[] {2300, 1800, 1800};
        courseSelectionOverlay.SetActive(true);
        DisplayCourses();
    }

    public void BusinessAndManagement()
    {
        selectedField = "Business and Management";
        fieldNameText.text = selectedField;

        courses = new string[] {"Bachelor of Science in Accountancy", 
                                "Bachelor of Science in Business Administration",
                                "Bachelor of Science in Business Economics",
                                "Bachelor of Science in Financial Management",
                                "Bachelor of Science in Hospitality Management",
                                "Bachelor of Science in Marketing Management",
                                "Bachelor of Science in Office Administration"};
        courseDuration = new float[] {2200, 1600, 1600, 1450, 1350, 1600, 1350};
        courseSelectionOverlay.SetActive(true);
        DisplayCourses();
    }

    public void Education()
    {
        selectedField = "Education";
        fieldNameText.text = selectedField;

        courses = new string[] {"Bachelor of Early Childhood Education", 
                                "Bachelor of Elementary Education",
                                "Bachelor of Secondary Education",
                                "Bachelor of Physical Education",
                                "Bachelor of Special Education",
                                "Bachelor of Technical Teacher Education"};
        courseDuration = new float[] {1450, 1800, 1800, 1450, 1450, 1600};
        courseSelectionOverlay.SetActive(true);
        DisplayCourses();
    }

    public void Engineering()
    {
        selectedField = "Engineering and Technology";
        fieldNameText.text = selectedField;

        courses = new string[] {"Bachelor of Science in Automotive Technology", 
                                "Bachelor of Science in Civil Engineering",
                                "Bachelor of Science in Chemical Engineering",
                                "Bachelor of Science in Electrical Engineering",
                                "Bachelor of Science in Geodetic Engineering",
                                "Bachelor of Science in Industrial Engineering",
                                "Bachelor of Science in Industrial Technology",
                                "Bachelor of Science in Mechanical Engineering"};
        courseDuration = new float[] {1700, 2200, 2200, 2200, 2000, 2000, 1500, 2200};
        courseSelectionOverlay.SetActive(true);
        DisplayCourses();
    }

    public void HealthSciences()
    {
        selectedField = "Health Sciences";
        fieldNameText.text = selectedField;

        courses = new string[] {"Bachelor of Science in Medical Technology", 
                                "Bachelor of Science in Midwifery",
                                "Bachelor of Science in Nursing",
                                "Bachelor of Science in Pharmacy",
                                "Bachelor of Science in Radiologic Technology"};
        courseDuration = new float[] {1800, 1450, 2000, 2200, 1800};
        courseSelectionOverlay.SetActive(true);
        DisplayCourses();
    }

    public void SocialSciences()
    {
        selectedField = "Social Sciences";
        fieldNameText.text = selectedField;

        courses = new string[] {"Bachelor of Science in Medical Technology", 
                                "Bachelor of Science in Midwifery",
                                "Bachelor of Science in Nursing",
                                "Bachelor of Science in Pharmacy",
                                "Bachelor of Science in Radiologic Technology"};
        courseDuration = new float[] {1450, 1500, 1450, 1350, 1500, 1450, 1350};
        courseSelectionOverlay.SetActive(true);
        DisplayCourses();
    }

    public void ScienceAndTech()
    {
        selectedField = "Science and Technology";
        fieldNameText.text = selectedField;

        courses = new string[] {"Bachelor of Science in Biology", 
                                "Bachelor of Science in Chemistry",
                                "Bachelor of Science in Computer Science",
                                "Bachelor of Science in Information Technology"};
        courseDuration = new float[] {1800, 1800, 1450, 1450};
        courseSelectionOverlay.SetActive(true);
        DisplayCourses();
    }

    public void DisplayCourses()
    {
        foreach (Transform child in courseButtonParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < courses.Length; i++)
        {
            GameObject newButton = Instantiate(courseButtonPrefab, courseButtonParent.transform);

            CourseButton courseButton = newButton.GetComponent<CourseButton>();
            courseButton.courseName.text = courses[i];
            courseButton.courseDuration.text = courseDuration[i].ToString() + " hrs";

            // Add an onClick listener to each button
            int index = i; // Create a local variable to hold the current index
            courseButton.GetComponent<Button>().onClick.AddListener(() => EnrollPrompt(index));
        }
    }

    public void EnrollPrompt(int index)
    {
        selectedCourse = courses[index];
        Debug.Log("Index = " + index);
        selectedCourseDuration = courseDuration[index];

        courseNamePrompt.text = selectedCourse;
        enrollPrompt.SetActive(true);
    }

    public void ContinueEnroll()
    {
        enrollPrompt.SetActive(false);
        courseSelectionOverlay.SetActive(false);
        universityOverlay.SetActive(true);

        Player.Instance.PlayerEnrolledCourse = selectedCourse;
        Player.Instance.PlayerEnrolledCourseDuration = selectedCourseDuration;

        course.text = Player.Instance.PlayerEnrolledCourse;
        duration.text = "/" + Player.Instance.PlayerEnrolledCourse.ToString();
        UpdateStudyHours(0);
    }
    public void CancelEnroll()
    {
        enrollPrompt.SetActive(false);
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

    public void UpdateStudyHours(float studyDurationValue)
    {
        Player.Instance.PlayerStudyHours += studyDurationValue;
        totalStudyHours.text = Player.Instance.PlayerStudyHours.ToString();
    }

    private IEnumerator ClosePanelCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        studyAnimationOverlay.SetActive(false);

    }
}
