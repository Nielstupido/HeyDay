using UnityEngine;

[CreateAssetMenu(menuName = "Job Position")]
public class JobPositions : ScriptableObject
{
    public string jobPosName;
    public string jobPosReqs;
    public StudyFields reqStudyField;
    public UniversityCourses reqCourse;
    public JobFields reqWorkField;
    public float reqWorkHrs;
    public float salaryPerHr;
}
