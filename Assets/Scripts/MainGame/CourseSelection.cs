using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public static class ButtonExtension
{
	public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
	{
		button.onClick.AddListener (delegate() {
			OnClick (param);
		});
	}
}
public class CourseSelection : MonoBehaviour
{
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private TextMeshProUGUI fieldName;
    
    private string[] courseList;
    private float[] courseDurationList;

    

    private void Start()
    {
        buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;

        for (int i = 0; i < courseList.Length; i++)
        {
            g = Instantiate (buttonTemplate, transform);
            g.transform.GetChild(0).GetComponent<Text>().text = courseList[i];
            g.transform.GetChild(1).GetComponent<Text>().text = courseDurationList[i].ToString() + " hrs";

            g.GetComponent <Button> ().AddEventListener (i, EnrollPrompt);
        }

        Destroy (buttonTemplate);
        
    }

    private void EnrollPrompt(int courseIndex)
	{
        //FindObjectOfType<University>().Enroll(courseList[courseIndex], courseDurationList[courseIndex]);
        University.Instance.Enroll(courseList[courseIndex], courseDurationList[courseIndex]);
	}

    public void AssignField(string fieldChosen)
    {
        fieldName.text = fieldChosen;
        if (fieldChosen == "Health Sciences")
        {
            courseList = new string[] { "Bachelor of Science in Medical Technology", 
                                        "Bachelor of Science in Midwifery",
                                        "Bachelor of Science in Nursing",
                                        "Bachelor of Science in Pharmacy",
                                        "Bachelor of Science in Radiologic Technology"};
                                        
            courseDurationList = new float[] {1800, 1450, 2000, 2200, 1800};
        }
        else if (fieldChosen == "Social Sciences")
        {
            courseList = new string[] { "Bachelor of Arts in Communication", 
                                        "Bachelor of Science in Criminal Justice",
                                        "Bachelor of Arts in Psychology",
                                        "Bachelor of Arts in Public Administration",
                                        "Bachelor of Arts in Political Science",
                                        "Bachelor of Arts in Sociology",
                                        "Bachelor of Science in Social Work"};
                                        
            courseDurationList = new float[] {1450, 1500, 1450, 1350, 1500, 1450, 1350};
        }
        else if (fieldChosen == "Architecture and Design")
        {
            courseList = new string[] { "Bachelor of Science in Architecture", 
                                        "Bachelor of Science in Interior Design",
                                        "Bachelor of Science in Industrial Design"};
                                        
            courseDurationList = new float[] {2300, 1800, 1800};
        }
        else if (fieldChosen == "Business and Management")
        {
            courseList = new string[] { "Bachelor of Science in Accountancy", 
                                        "Bachelor of Science in Business Administration",
                                        "Bachelor of Science in Business Economics",
                                        "Bachelor of Science in Financial Management",
                                        "Bachelor of Science in Hospitality Management",
                                        "Bachelor of Science in Marketing Management",
                                        "Bachelor of Science in Office Administration"};
                                        
            courseDurationList = new float[] {2200, 1600, 1600, 1450, 1350, 1600, 1350};
        }
        else if (fieldChosen == "Education")
        {
            courseList = new string[] { "Bachelor of Early Childhood Education", 
                                        "Bachelor of Elementary Education",
                                        "Bachelor of Secondary Education",
                                        "Bachelor of Physical Education",
                                        "Bachelor of Special Education",
                                        "Bachelor of Technical Teacher Education"};
                                        
            courseDurationList = new float[] {1450, 1800, 1800, 1450, 1450, 1600};
        }
        else if (fieldChosen == "Science and Technology")
        {
            courseList = new string[] { "Bachelor of Science in Biology", 
                                        "Bachelor of Science in Chemistry",
                                        "Bachelor of Science in Computer Science",
                                        "Bachelor of Science in Information Technology"};
                                        
            courseDurationList = new float[] {1800, 1800, 1450, 1450};
        }
        else if (fieldChosen == "Engineering and Technology")
        {
            courseList = new string[] { "Bachelor of Science in Automotive Technology", 
                                        "Bachelor of Science in Civil Engineering",
                                        "Bachelor of Science in Chemical Engineering",
                                        "Bachelor of Science in Electrical Engineering",
                                        "Bachelor of Science in Geodetic Engineering",
                                        "Bachelor of Science in Industrial Engineering",
                                        "Bachelor of Science in Industrial Technology",
                                        "Bachelor of Science in Mechanical Engineering"};
                                        
            courseDurationList = new float[] {1700, 2200, 2200, 2200, 2000, 2000, 1500, 2200};
        }
    }
}
