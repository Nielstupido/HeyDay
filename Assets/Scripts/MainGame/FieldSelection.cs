using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FieldSelection : MonoBehaviour
{
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private GameObject courseListOverlay;
    [SerializeField] private GameObject fieldGridOverlay;

    private string[] fieldList = new string[] { "Architecture and Design", 
                                                "Business and Management",
                                                "Education",
                                                "Engineering and Technology",
                                                "Health Sciences",
                                                "Social Sciences",
                                                "Science and Technology"};

    private void Start()
    {
        buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;

        for (int i = 0; i < fieldList.Length; i++)
        {
            g = Instantiate (buttonTemplate, transform);
            g.transform.GetChild(0).GetComponent<Text>().text = fieldList[i];

            g.GetComponent <Button> ().AddEventListener (i, SelectField);
        }

        Destroy (buttonTemplate);
    }

    private void SelectField(int index)
    {
        courseListOverlay.SetActive(true);
        FindObjectOfType<CourseSelection>().AssignField(fieldList[index]);
        fieldGridOverlay.SetActive(false);
    }
}
