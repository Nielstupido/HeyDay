using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum Animations
{
    SLEEPING,
    WORKING,
    TRAVELING,
    RESIGNING,
    JOBAPPLICATIONPROCESSING
}


public class AnimOverlayManager : MonoBehaviour
{
    [SerializeField] private GameObject animationOverlay;
    [SerializeField] private TextMeshProUGUI tempAnimText;
    public static AnimOverlayManager Instance {private set; get;}


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


    public void StartAnim(Animations anim)
    {
        switch (anim)
        {
            case Animations.SLEEPING:
                tempAnimText.text = "Sleeping...";
                break;
            case Animations.WORKING:
                tempAnimText.text = "Working...";
                break;
            case Animations.TRAVELING:
                tempAnimText.text = "Traveling...";
                break;
            case Animations.RESIGNING:
                tempAnimText.text = "Processing Resignation...";
                break;
            case Animations.JOBAPPLICATIONPROCESSING:
                tempAnimText.text = "Processing Job Application...";
                break;
        }

        animationOverlay.SetActive(true);
    }


    public void StopAnim()
    {
        animationOverlay.SetActive(false);
    }
}
