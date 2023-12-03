using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    [SerializeField] private GameObject qw;


    public void Chat()
    {

    }

    // private void ProceedResignation()
    // {
    //     JobManager.Instance.ArrangeResignation(this.gameObject, jobSystemOverlay);
    // }
    

    // private IEnumerator ChattingAnim(float waitingTime)
    // {
    //     AnimOverlayManager.Instance.StartAnim(ActionAnimations.CHAT);
    //     yield return new WaitForSeconds(waitingTime);
    //     resignationOverlay.SetActive(false);
    //     AnimOverlayManager.Instance.StopAnim();
    //     ProceedResignation();
    //     yield return null;
    // }
}
