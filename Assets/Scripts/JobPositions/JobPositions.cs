using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "Job Position")]
[Serializable]
public class JobPositions : ScriptableObject
{
    public string jobPosName;
    public string jobPosReqs;
    public Buildings establishment;
    public JobFields workField;
    public StudyFields reqStudyField;
    public List<UniversityCourses> reqCourse;
    public JobFields reqWorkField;
    public float reqWorkHrs;
    public float salaryPerHr;
}
