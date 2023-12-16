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
    [SerializeField] private CanvasGroup loadScreenFadeOverlay;
    [SerializeField] private CanvasGroup whiteScreenFadeOverlay;
    [SerializeField] private CanvasGroup blackScreenFadeOverlay;
    [SerializeField] private Image animationImage;
    [SerializeField] private Animator animator;
    [SerializeField] private List<AnimationScriptableObject> animationObjects = new List<AnimationScriptableObject>();
    private float localDelayTime;
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
        AudioManager.Instance.StopSFX();
    }


    public void StartScreenFadeLoadScreen()
    {
        localDelayTime = 2f;
        loadScreenFadeOverlay.alpha = 0f;
        blackScreenFadeOverlay.alpha = 0f;
        whiteScreenFadeOverlay.alpha = 0f;
        screenFadeCanvas.SetActive(true);
        loadScreenFadeOverlay.LeanAlpha(1f, 0.5f).setOnComplete(FadeOutScreen);
    }


    public void StartScreenFadeLoadScreen(float delayTime)
    {
        localDelayTime = delayTime;
        loadScreenFadeOverlay.alpha = 0f;
        screenFadeCanvas.SetActive(true);
        loadScreenFadeOverlay.LeanAlpha(1f, 0.5f).setOnComplete(FadeOutScreen);
    }


    private void FadeOutScreen()
    {
        loadScreenFadeOverlay.LeanAlpha(0f, 0.5f).setOnComplete( () => {screenFadeCanvas.SetActive(false);} ).delay = localDelayTime;
    }


    public void StartWhiteScreenFadeLoadScreen()
    {
        whiteScreenFadeOverlay.alpha = 0f;
        blackScreenFadeOverlay.alpha = 0f;
        loadScreenFadeOverlay.alpha = 0f;
        screenFadeCanvas.SetActive(true);
        whiteScreenFadeOverlay.LeanAlpha(1f, 0.5f).setOnComplete(FadeOutWhiteScreen);
    }


    private void FadeOutWhiteScreen()
    {
        whiteScreenFadeOverlay.LeanAlpha(0f, 0.5f).setOnComplete( () => {screenFadeCanvas.SetActive(false);} ).delay = 1f;
    }


    public void StartBlackScreenFadeLoadScreen()
    {
        blackScreenFadeOverlay.alpha = 0f;
        whiteScreenFadeOverlay.alpha = 0f;
        loadScreenFadeOverlay.alpha = 0f;
        screenFadeCanvas.SetActive(true);
        blackScreenFadeOverlay.LeanAlpha(1f, 0.5f).setOnComplete(FadeOutBlackScreen);
    }


    private void FadeOutBlackScreen()
    {
        blackScreenFadeOverlay.LeanAlpha(0f, 0.5f).setOnComplete( () => {screenFadeCanvas.SetActive(false);} ).delay = 0.2f;
    }
}
