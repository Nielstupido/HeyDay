using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;

public class GoalSetter : MonoBehaviour
{
    [SerializeField] private GameObject goalSetterOverlay;
    [SerializeField] private TextMeshProUGUI educGoalText;
    [SerializeField] private TextMeshProUGUI narrativeText;
    private List<UniversityCourses> courseList = new List<UniversityCourses>();
    private int randomNum;
    public static GoalSetter Instance { get; private set; }


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


    public void SetGoal()
    {
        goalSetterOverlay.SetActive(true);
        OverlayAnimations.Instance.ShowGoalSetter();
        GenerateGoal();
    }


    public void StartGame()
    {
        AudioManager.Instance.PlaySFX("Select");
        goalSetterOverlay.SetActive(false);
        GameManager.Instance.StartLevel();
    }


    private void GenerateGoal()
    {
        narrativeText.text = "	You are " + Player.Instance.PlayerName + ", a 19-year-old " + Player.Instance.PlayerGender.ToString() + " who decided to pursue a degree in " +
                            "HeyDay City with only P5,000 in your pocket. You plan to get a degree, get " +
                            "a part- job, and save enough money to be able to continue living in the city " +
                            "after graduation. Your main goals are:";
        
        courseList = Enum.GetValues(typeof(UniversityCourses)).Cast<UniversityCourses>().ToList();
        randomNum = UnityEngine.Random.Range(0, courseList.Count);
        Player.Instance.GoalCourse = courseList[randomNum];
        educGoalText.text = GameManager.Instance.EnumStringParser(courseList[randomNum]);
    }
}
