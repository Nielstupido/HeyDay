using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum RelStatus
{
    STRANGERS,
    FRIENDS,
    GOOD_FRIENDS,
    BEST_BUDDIES,
    ENEMIES
} 


public class InteractionSystemManager : MonoBehaviour
{
    [SerializeField] private GameObject interactionOverlay;
    [SerializeField] private GameObject touchBlockerOverlay;
    [SerializeField] private GameObject payDebtBtn;
    [SerializeField] private GameObject getContactNumBtn;
    [SerializeField] private GameObject speechBubbleImage;
    [SerializeField] private TextMeshProUGUI debtValue;
    [SerializeField] private TextMeshProUGUI speechBubbleText;
    [SerializeField] private Slider relStatusBar;


    public void Chat()
    {
        StartCoroutine(ChattingAnim(2f));
    }


    public void Hug()
    {
        StartCoroutine(ChattingAnim(2f));
    }


    public void SayBye()
    {
        StartCoroutine(ChattingAnim(2f));
    }


    private void OnDoneChatting()
    {
        TimeManager.Instance.AddClockTime(0.1f);
    }
    

    private IEnumerator ChattingAnim(float waitingTime)
    {
        AnimOverlayManager.Instance.StartAnim(ActionAnimations.CHAT);
        yield return new WaitForSeconds(waitingTime);
        AnimOverlayManager.Instance.StopAnim();
        OnDoneChatting();
        yield return null;
    }
}
