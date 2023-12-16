using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public enum StudyFields
{
    NONE,
    Architecture_and_Design,
    Business_and_Management,
    Education,
    Engineering_and_Technology,
    Health_Sciences,
    Social_Sciences,
    Science_and_Technology
}


public enum UniversityCourses
{
    NONE,
    ANY,

    Bachelor_of_Science_in_Architecture,
    Bachelor_of_Science_in_Interior_Design,
    Bachelor_of_Science_in_Industrial_Design,

    Bachelor_of_Science_in_Accountancy,
    Bachelor_of_Science_in_Business_Administration,
    Bachelor_of_Science_in_Business_Economics,
    Bachelor_of_Science_in_Financial_Management,
    Bachelor_of_Science_in_Hospitality_Management,
    Bachelor_of_Science_in_Marketing_Management,
    Bachelor_of_Science_in_Office_Administration,

    Bachelor_of_Early_Childhood_Education,
    Bachelor_of_Elementary_Education,
    Bachelor_of_Secondary_Education,
    Bachelor_of_Physical_Education,
    Bachelor_of_Special_Education,
    Bachelor_of_Technical_Teacher_Education,

    Bachelor_of_Science_in_Automotive_Technology,
    Bachelor_of_Science_in_Civil_Engineering,
    Bachelor_of_Science_in_Chemical_Engineering,
    Bachelor_of_Science_in_Electrical_Engineering,
    Bachelor_of_Science_in_Geodetic_Engineering,
    Bachelor_of_Science_in_Industrial_Engineering,
    Bachelor_of_Science_in_Industrial_Technology,
    Bachelor_of_Science_in_Mechanical_Engineering,

    Bachelor_of_Science_in_Medical_Technology,
    Bachelor_of_Science_in_Midwifery,
    Bachelor_of_Science_in_Nursing,
    Bachelor_of_Science_in_Pharmacy,
    Bachelor_of_Science_in_Radiologic_Technology,

    Bachelor_of_Arts_in_Communication,
    Bachelor_of_Science_in_Criminal_Justice,
    Bachelor_of_Arts_in_Psychology,
    Bachelor_of_Arts_in_Public_Administration,
    Bachelor_of_Arts_in_Political_Science,
    Bachelor_of_Arts_in_Sociology,
    Bachelor_of_Science_in_Social_Work,

    Bachelor_of_Science_in_Biology,
    Bachelor_of_Science_in_Chemistry,
    Bachelor_of_Science_in_Computer_Science,
    Bachelor_of_Science_in_Information_Technology
}



public class UniversityManager : MonoBehaviour
{
    [SerializeField] private GameObject universitySystemOverlay;
    [SerializeField] private TextMeshProUGUI course;
    [SerializeField] private TextMeshProUGUI duration;
    [SerializeField] private TextMeshProUGUI totalStudyHours;

    [SerializeField] private GameObject fieldSelectionOverlay;
    [SerializeField] private GameObject fieldSelectionPopUp;
    [SerializeField] private TextMeshProUGUI fieldNameText; 
    [SerializeField] private GameObject courseSelectionOverlay;
    [SerializeField] private GameObject coursesPopUp;
    [SerializeField] private GameObject courseButtonPrefab;
    [SerializeField] private GameObject courseButtonParent;

    [SerializeField] private GameObject studyPromptOverlay;
    [SerializeField] private TextMeshProUGUI studyDurationText;

    [SerializeField] private GameObject enrollPrompt;
    [SerializeField] private GameObject enrollPopUp;
    [SerializeField] private TextMeshProUGUI courseNamePrompt;

    [SerializeField] private University university;
    [SerializeField] private GameObject archiBtn;
    [SerializeField] private GameObject bsmBtn;
    [SerializeField] private GameObject educBtn;
    [SerializeField] private GameObject engrBtn;
    [SerializeField] private GameObject medBtn;
    [SerializeField] private GameObject socSciBtn;
    [SerializeField] private GameObject sciTechBtn;


    private StudyFields selectedField; 
    private UniversityCourses selectedCourse;
    private float selectedCourseDuration;
    private UniversityCourses[] courses;
    private float[] courseDuration;
    private int studyDuration;
 
    public static UniversityManager Instance { get; private set; }


    private void Awake() 
    { 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void ArchitectureAndDesign()
    {
        AudioManager.Instance.PlaySFX("Select");
        selectedField = StudyFields.Architecture_and_Design;
        fieldNameText.text = GameManager.Instance.EnumStringParser(selectedField);

        courses = new UniversityCourses[] {UniversityCourses.Bachelor_of_Science_in_Architecture, 
                                UniversityCourses.Bachelor_of_Science_in_Interior_Design,
                                UniversityCourses.Bachelor_of_Science_in_Industrial_Design};
        courseDuration = new float[] {2300, 1800, 1800};
        DisplayCourses();
    }


    public void BusinessAndManagement()
    {
        AudioManager.Instance.PlaySFX("Select");
        selectedField = StudyFields.Business_and_Management;
        fieldNameText.text = GameManager.Instance.EnumStringParser(selectedField);

        courses = new UniversityCourses[] {UniversityCourses.Bachelor_of_Science_in_Accountancy, 
                                UniversityCourses.Bachelor_of_Science_in_Business_Administration,
                                UniversityCourses.Bachelor_of_Science_in_Business_Economics,
                                UniversityCourses.Bachelor_of_Science_in_Financial_Management,
                                UniversityCourses.Bachelor_of_Science_in_Hospitality_Management,
                                UniversityCourses.Bachelor_of_Science_in_Marketing_Management,
                                UniversityCourses.Bachelor_of_Science_in_Office_Administration};
        courseDuration = new float[] {2200, 1600, 1600, 1450, 1350, 1600, 1350};
        DisplayCourses();
    }


    public void Education()
    {
        AudioManager.Instance.PlaySFX("Select");
        selectedField = StudyFields.Education;
        fieldNameText.text = GameManager.Instance.EnumStringParser(selectedField);

        courses = new UniversityCourses[] {UniversityCourses.Bachelor_of_Early_Childhood_Education, 
                                UniversityCourses.Bachelor_of_Elementary_Education,
                                UniversityCourses.Bachelor_of_Secondary_Education,
                                UniversityCourses.Bachelor_of_Physical_Education,
                                UniversityCourses.Bachelor_of_Special_Education,
                                UniversityCourses.Bachelor_of_Technical_Teacher_Education};
        courseDuration = new float[] {1450, 1800, 1800, 1450, 1450, 1600};
        DisplayCourses();
    }


    public void Engineering()
    {
        AudioManager.Instance.PlaySFX("Select");
        selectedField = StudyFields.Engineering_and_Technology;
        fieldNameText.text = GameManager.Instance.EnumStringParser(selectedField);

        courses = new UniversityCourses[] {UniversityCourses.Bachelor_of_Science_in_Automotive_Technology, 
                                UniversityCourses.Bachelor_of_Science_in_Civil_Engineering,
                                UniversityCourses.Bachelor_of_Science_in_Chemical_Engineering,
                                UniversityCourses.Bachelor_of_Science_in_Electrical_Engineering,
                                UniversityCourses.Bachelor_of_Science_in_Geodetic_Engineering,
                                UniversityCourses.Bachelor_of_Science_in_Industrial_Engineering,
                                UniversityCourses.Bachelor_of_Science_in_Industrial_Technology,
                                UniversityCourses.Bachelor_of_Science_in_Mechanical_Engineering};
        courseDuration = new float[] {1700, 2200, 2200, 2200, 2000, 2000, 1500, 2200};
        DisplayCourses();
    }


    public void HealthSciences()
    {
        AudioManager.Instance.PlaySFX("Select");
        selectedField = StudyFields.Health_Sciences;
        fieldNameText.text = GameManager.Instance.EnumStringParser(selectedField);

        courses = new UniversityCourses[] {UniversityCourses.Bachelor_of_Science_in_Medical_Technology, 
                                UniversityCourses.Bachelor_of_Science_in_Midwifery,
                                UniversityCourses.Bachelor_of_Science_in_Nursing,
                                UniversityCourses.Bachelor_of_Science_in_Pharmacy,
                                UniversityCourses.Bachelor_of_Science_in_Radiologic_Technology};
        courseDuration = new float[] {1800, 1450, 2000, 2200, 1800};
        DisplayCourses();
    }


    public void SocialSciences()
    {
        AudioManager.Instance.PlaySFX("Select");
        selectedField = StudyFields.Social_Sciences;
        fieldNameText.text = GameManager.Instance.EnumStringParser(selectedField);

        courses = new UniversityCourses[] {UniversityCourses.Bachelor_of_Arts_in_Communication, 
                                UniversityCourses.Bachelor_of_Science_in_Criminal_Justice,
                                UniversityCourses.Bachelor_of_Arts_in_Psychology,
                                UniversityCourses.Bachelor_of_Arts_in_Public_Administration,
                                UniversityCourses.Bachelor_of_Arts_in_Political_Science,
                                UniversityCourses.Bachelor_of_Arts_in_Sociology,
                                UniversityCourses.Bachelor_of_Science_in_Social_Work};
        courseDuration = new float[] {1450, 1500, 1450, 1350, 1500, 1450, 1350};
        DisplayCourses();
    }


    public void ScienceAndTech()
    {
        AudioManager.Instance.PlaySFX("Select");
        selectedField = StudyFields.Science_and_Technology;
        fieldNameText.text = GameManager.Instance.EnumStringParser(selectedField);

        courses = new UniversityCourses[] {UniversityCourses.Bachelor_of_Science_in_Biology, 
                                UniversityCourses.Bachelor_of_Science_in_Chemistry,
                                UniversityCourses.Bachelor_of_Science_in_Computer_Science,
                                UniversityCourses.Bachelor_of_Science_in_Information_Technology};
        courseDuration = new float[] {1800, 1800, 1450, 1450};
        DisplayCourses();
    }


    public void OnEnteredUniversity()
    {
        AudioManager.Instance.PlaySFX("Select");
        universitySystemOverlay.SetActive(true);
        UpdateCourseDetsHUD();
    }


    public void OnExitedUniversity()
    {
        AudioManager.Instance.PlaySFX("Select");
        universitySystemOverlay.SetActive(false);
    }

    public void HideFieldSelection()
    {
        fieldSelectionOverlay.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(fieldSelectionPopUp, fieldSelectionOverlay);
    }


    public void ShowEnrollOverlay()
    {
        AudioManager.Instance.PlaySFX("Select");
        fieldSelectionOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(fieldSelectionPopUp);
    }


    public void ShowStudyOverlay()
    {
        AudioManager.Instance.PlaySFX("Select");
        studyPromptOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(studyPromptOverlay);
    }

    
    private void DisplayCourses()
    {
        courseSelectionOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(coursesPopUp);

        foreach (Transform child in courseButtonParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < courses.Length; i++)
        {
            GameObject newButton = Instantiate(courseButtonPrefab, courseButtonParent.transform);

            CourseButton courseButton = newButton.GetComponent<CourseButton>();
            courseButton.courseName.text = GameManager.Instance.EnumStringParser(courses[i]);
            courseButton.courseDuration.text = courseDuration[i].ToString() + " hrs";
            
            int index = i; 
            courseButton.GetComponent<Button>().onClick.AddListener(() => EnrollPrompt(index));
        }
    }

    public void HideCourseSelection()
    {
        courseSelectionOverlay.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(coursesPopUp, courseSelectionOverlay);
    }


    public void EnrollPrompt(int index)
    {
        selectedCourse = courses[index];
        selectedCourseDuration = courseDuration[index];

        courseNamePrompt.text = GameManager.Instance.EnumStringParser(selectedCourse);
        enrollPrompt.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(enrollPopUp);
    }


    public void ContinueEnroll()
    {
        AudioManager.Instance.PlaySFX("Select");
        if (selectedCourse == Player.Instance.GoalCourse)
        {
            try
            {
                LevelManager.onFinishedPlayerAction(MissionType.ENROLL);
            }
            catch (System.Exception e)
            {
                Debugger.Instance.ShowError(e.ToString());
            }
        }
        
        enrollPrompt.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(enrollPopUp, enrollPrompt);
        courseSelectionOverlay.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(coursesPopUp, courseSelectionOverlay);
        fieldSelectionOverlay.SetActive(false);
        Player.Instance.PlayerEnrolledStudyField = selectedField;
        Player.Instance.PlayerEnrolledCourse = selectedCourse;
        Player.Instance.PlayerEnrolledCourseDuration = selectedCourseDuration;
        Player.Instance.PlayerStudyHours = 0f;
        UpdateCourseDetsHUD();
        UpdateStudyHours(0);
        BuildingManager.Instance.PrepareButtons(BuildingManager.Instance.CurrentSelectedBuilding);
    }


    public void CancelEnroll()
    {
        AudioManager.Instance.PlaySFX("Select");
        enrollPrompt.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(enrollPopUp, enrollPrompt);
    }


    public void ContinueStudy()
    {
        AudioManager.Instance.PlaySFX("Select");
        Player.Instance.Study(studyDuration);
        studyPromptOverlay.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(studyPromptOverlay, studyPromptOverlay);
        studyDurationText.text = "1";
        StartCoroutine(EndStudyAnimation(3));
    }


    public void CancelStudy()
    {
        AudioManager.Instance.PlaySFX("Select");
        studyPromptOverlay.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(studyPromptOverlay, studyPromptOverlay);
    }


    public void UpdateStudyHours(float studyDurationValue)
    {
        Player.Instance.PlayerStudyHours += studyDurationValue;
        totalStudyHours.text = Player.Instance.PlayerStudyHours.ToString();
    }


    private void UpdateCourseDetsHUD()
    {
        course.text = GameManager.Instance.EnumStringParser(selectedCourse);
        duration.text = "/" + selectedCourseDuration.ToString();
    }


    public void IncrementStudyHrs()
    {
        AudioManager.Instance.PlaySFX("Select");
        studyDuration = int.Parse(studyDurationText.text);
        if (studyDuration < ((university.buildingClosingTime - TimeManager.Instance.CurrentTime)))
        {
            studyDurationText.text = (++studyDuration).ToString();
        }
    }

    
    public void DecrementStudyHrs()
    {
        AudioManager.Instance.PlaySFX("Select");
        studyDuration = int.Parse(studyDurationText.text);
        if (studyDuration > 0f)
        {
            studyDurationText.text = (--studyDuration).ToString();
        }
    }

    private IEnumerator EndStudyAnimation(float seconds)
    {
        AudioManager.Instance.PlaySFX("Study");
        AnimOverlayManager.Instance.StartAnim(ActionAnimations.STUDY);
        yield return new WaitForSeconds(seconds);
        AnimOverlayManager.Instance.StopAnim();
    }
}
