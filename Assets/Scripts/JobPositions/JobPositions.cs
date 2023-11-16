using UnityEngine;

[CreateAssetMenu(menuName = "Job Position")]
public class JobPositions : ScriptableObject
{
    public string jobPosName;
    public string jobPosDets;
    public StudyFields reqField;
    public UniversityCourses reqCourse;
    public float reqWorkHrs;
    public float salaryPerHr;
}
