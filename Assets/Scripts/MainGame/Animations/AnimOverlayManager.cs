using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public enum ActionAnimations
{
    SLEEP,
    WORK,
    RESIGN,
    JOBAPPLICATION,
    PARTY,
    DRIVE,
    COMMUTE,
    BARBER,
    SALON,
    SPA,
    WATCHMOVIE,
    GYM,
    DENTALSERVICE,
    EAT,
    STUDY,
    CHAT, 
    BUY,
    SALARY, 
    WALK,
    DIAL
}


public class AnimOverlayManager : MonoBehaviour
{
    [SerializeField] private GameObject animationOverlay;
    [SerializeField] private GameObject screenFadeCanvas;
    [SerializeField] private CanvasGroup screenFadeOverlay;
    [SerializeField] private Image animationImage;
    [SerializeField] private Animator animator;
    [SerializeField] private List<AnimationScriptableObject> animationObjects = new List<AnimationScriptableObject>();
    public static AnimOverlayManager Instance {private set; get;}


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


    public void StartAnim(ActionAnimations currentActionAnim)
    {
        if (!animationObjects.Exists((animObj) => animObj.actionAnimation == currentActionAnim))
        {
            return;
        }
        animationImage.sprite = animationObjects.Find((animObj) => animObj.actionAnimation == currentActionAnim).firstImage;
        animationImage.SetNativeSize();
        animator.runtimeAnimatorController = animationObjects.Find((animObj) => animObj.actionAnimation == currentActionAnim).animController;
        animationOverlay.SetActive(true);
    }


    public void StopAnim()
    {
        animationOverlay.SetActive(false);
    }


    public void StartScreenFadeLoadScreen()
    {
        screenFadeCanvas.SetActive(true);
        screenFadeOverlay.alpha = 0f;
        screenFadeOverlay.LeanAlpha(1f, 0.5f).setOnComplete(FadeOutScreen);
    }


    private void FadeOutScreen()
    {
        screenFadeOverlay.LeanAlpha(0f, 0.5f).setOnComplete( () => {screenFadeCanvas.SetActive(false);} ).delay = 1f;
    }
}
