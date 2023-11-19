using UnityEngine;

[CreateAssetMenu(menuName = "Job Position")]
public class JobPositions : ScriptableObject
{
    public string jobPosName;
    public string jobPosReqs;
    public Buildings establishment;
    public JobFields workField;
    public StudyFields reqStudyField;
    public UniversityCourses reqCourse;
    public JobFields reqWorkField;
    public float reqWorkHrs;
    public float salaryPerHr;
}
