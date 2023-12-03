using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


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
    private CharactersScriptableObj interactingCharacter;
    private string npcGreetings;
    private ValueTuple<bool, int, string> response;


    public void Interact(CharactersScriptableObj character)
    {
        interactionOverlay.SetActive(true);
        interactingCharacter = character;
        npcGreetings = interactingCharacter.Interact();
        if (npcGreetings != null)
        {
            StartCoroutine(GreetPlayer());
        }
    }


    public void EndInteraction()
    {
        interactingCharacter.OnInteractionEnded();
        interactingCharacter = null;
        speechBubbleImage.SetActive(false);
        interactionOverlay.SetActive(false);
    }


    public void Chat()
    {
        response = interactingCharacter.Chat();
        if ((interactingCharacter.Chat()).Item1)
        {
            StartCoroutine(ChattingAnim(2f));
        }
    }


    public void Hug()
    {
    }


    public void SayBye()
    {
    }


    public void PayDebt()
    {
    }


    public void AskForContactNum()
    {
    }


    public void YellAt()
    {
    }


    public void TellJoke()
    {
    }


    public void BorrowMoney()
    {
    }


    private void OnDoneChatting()
    {
        UpdateInteractionUI();
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


    private IEnumerator GreetPlayer()
    {
        speechBubbleImage.SetActive(true);
        speechBubbleText.text = npcGreetings;
        yield return new WaitForSeconds(1.5f);
        speechBubbleImage.SetActive(false);
        yield return null;
    }


    private void UpdateInteractionUI()
    {
        relStatusBar.value = interactingCharacter.relStatBarValue;
    }
}
