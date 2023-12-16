using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayAnimations : MonoBehaviour
{
    [SerializeField] GameObject
    goalAssignmentOverlay,
    phoneObj,
    movieTicket,
    barTicket;


    public static OverlayAnimations Instance { get; private set; }


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

    public void AnimOpenOverlay(GameObject obj)
    {
        LeanTween.scale(obj, new Vector3(1f,1f,1f),0.5f).setDelay(0.5f).setEase(LeanTweenType.easeOutElastic);
    }

    public void AnimCloseOverlay(GameObject obj, GameObject panel)
    {
        LeanTween.scale(obj, new Vector3(0f,0f,0f),0.5f)
        .setEase(LeanTweenType.easeInElastic)
        .setOnComplete(() => panel.SetActive(false));
    }

    public void AnimShowObj(GameObject obj)
    {
        LeanTween.scale(obj, new Vector3(1f,1f,1f),0.5f).setEase(LeanTweenType.easeOutBounce);
    }

    public void AnimHideObj(GameObject obj, GameObject panel)
    {
        LeanTween.scale(obj, new Vector3(0f,0f,0f),0.5f)
        .setEase(LeanTweenType.easeInBounce)
        .setOnComplete(() => panel.SetActive(false));
    }

    public void ShowGoalSetter()
    {
        LeanTween.scale(goalAssignmentOverlay, new Vector3(1f,1f,1f),2f)
        .setDelay(0.5f)
        .setEase(LeanTweenType.easeInOutBack);
    }

    public void ShowPhone()
    {
        LeanTween.scale(phoneObj, new Vector3(1f,1f,1f),0.2f)
        .setEase(LeanTweenType.easeInBounce);
    }

    public void HidePhone(GameObject panel)
    {
        LeanTween.scale(phoneObj, new Vector3(0f,0f,0f),0.2f)
        .setEase(LeanTweenType.easeOutBounce)
        .setOnComplete(() => panel.SetActive(false));
    }

    public void ShowMovieTicket()
    {
        LeanTween.scale(movieTicket, new Vector3(1f,1f,1f),0.5f)
        .setDelay(0.5f)
        .setEase(LeanTweenType.easeOutElastic);
    }

    public void ShowBarTicket()
    {
        LeanTween.scale(barTicket, new Vector3(1f,1f,1f),0.5f)
        .setDelay(0.5f)
        .setEase(LeanTweenType.easeOutElastic);
    }

}
