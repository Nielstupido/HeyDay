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

    private StudyFields selectedField; 
    private UniversityCourses selectedCourse;
    private float selectedCourseDuration;
    private UniversityCourses[] courses;
    private float[] courseDuration;
 
    public GameObject StudyPromptOverlay {get{return studyPromptOverlay;}}
    public GameObject FieldSelectionOverlay {get{return fieldSelectionOverlay;}}
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
        selectedField = StudyFields.Architecture_and_Design;
        fieldNameText.text = GameManager.Instance.EnumStringParser(selectedField);

        courses = new UniversityCourses[] {UniversityCourses.Bachelor_of_Science_in_Architecture, 
                                UniversityCourses.Bachelor_of_Science_in_Interior_Design,
                                UniversityCourses.Bachelor_of_Science_in_Industrial_Design};
        courseDuration = new float[] {2300, 1800, 1800};
        courseSelectionOverlay.SetActive(true);
        DisplayCourses();
    }


    public void BusinessAndManagement()
    {
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
        courseSelectionOverlay.SetActive(true);
        DisplayCourses();
    }


    public void Education()
    {
        selectedField = StudyFields.Education;
        fieldNameText.text = GameManager.Instance.EnumStringParser(selectedField);

        courses = new UniversityCourses[] {UniversityCourses.Bachelor_of_Early_Childhood_Education, 
                                UniversityCourses.Bachelor_of_Elementary_Education,
                                UniversityCourses.Bachelor_of_Secondary_Education,
                                UniversityCourses.Bachelor_of_Physical_Education,
                                UniversityCourses.Bachelor_of_Special_Education,
                                UniversityCourses.Bachelor_of_Technical_Teacher_Education};
        courseDuration = new float[] {1450, 1800, 1800, 1450, 1450, 1600};
        courseSelectionOverlay.SetActive(true);
        DisplayCourses();
    }


    public void Engineering()
    {
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
        courseSelectionOverlay.SetActive(true);
        DisplayCourses();
    }


    public void HealthSciences()
    {
        selectedField = StudyFields.Health_Sciences;
        fieldNameText.text = GameManager.Instance.EnumStringParser(selectedField);

        courses = new UniversityCourses[] {UniversityCourses.Bachelor_of_Science_in_Medical_Technology, 
                                UniversityCourses.Bachelor_of_Science_in_Midwifery,
                                UniversityCourses.Bachelor_of_Science_in_Nursing,
                                UniversityCourses.Bachelor_of_Science_in_Pharmacy,
                                UniversityCourses.Bachelor_of_Science_in_Radiologic_Technology};
        courseDuration = new float[] {1800, 1450, 2000, 2200, 1800};
        courseSelectionOverlay.SetActive(true);
        DisplayCourses();
    }


    public void SocialSciences()
    {
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
        courseSelectionOverlay.SetActive(true);
        DisplayCourses();
    }


    public void ScienceAndTech()
    {
        selectedField = StudyFields.Science_and_Technology;
        fieldNameText.text = GameManager.Instance.EnumStringParser(selectedField);

        courses = new UniversityCourses[] {UniversityCourses.Bachelor_of_Science_in_Biology, 
                                UniversityCourses.Bachelor_of_Science_in_Chemistry,
                                UniversityCourses.Bachelor_of_Science_in_Computer_Science,
                                UniversityCourses.Bachelor_of_Science_in_Information_Technology};
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
            courseButton.courseName.text = GameManager.Instance.EnumStringParser(courses[i]);
            courseButton.courseDuration.text = courseDuration[i].ToString() + " hrs";
            
            int index = i; 
            courseButton.GetComponent<Button>().onClick.AddListener(() => EnrollPrompt(index));
        }
    }


    public void EnrollPrompt(int index)
    {
        selectedCourse = courses[index];
        selectedCourseDuration = courseDuration[index];

        courseNamePrompt.text = GameManager.Instance.EnumStringParser(selectedCourse);
        enrollPrompt.SetActive(true);
    }


    public void ContinueEnroll()
    {
        enrollPrompt.SetActive(false);
        courseSelectionOverlay.SetActive(false);
        fieldSelectionOverlay.SetActive(false);
        Player.Instance.PlayerEnrolledStudyField = selectedField;
        Player.Instance.PlayerEnrolledCourse = selectedCourse;
        Player.Instance.PlayerEnrolledCourseDuration = selectedCourseDuration;
        course.text = GameManager.Instance.EnumStringParser(selectedCourse);
        duration.text = "/" + selectedCourseDuration.ToString();
        universityOverlay.SetActive(true);
        UpdateStudyHours(0);
    }


    public void CancelEnroll()
    {
        enrollPrompt.SetActive(false);
    }


    public void ContinueStudy()
    {
        float studyDuration = CheckInputDuration(float.Parse(studyDurationField.text));
        Player.Instance.Study(studyDuration);
        studyPromptOverlay.SetActive(false);
        studyAnimationOverlay.SetActive(true);

        StartCoroutine(EndStudyAnimation(3));
    }


    public void CancelStudy()
    {
        studyPromptOverlay.SetActive(false);
    }


    public float CheckInputDuration(float duration)
    {
        float targetTime = duration + TimeManager.Instance.CurrentTime;
        if (targetTime > 17.0f)
        {
            float excessTime = targetTime - 17;
            duration -= excessTime;
        }

        return duration;
    }


    public void UpdateStudyHours(float studyDurationValue)
    {
        Player.Instance.PlayerStudyHours += studyDurationValue;
        totalStudyHours.text = Player.Instance.PlayerStudyHours.ToString();
    }


    private IEnumerator EndStudyAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        studyAnimationOverlay.SetActive(false);

    }
}
