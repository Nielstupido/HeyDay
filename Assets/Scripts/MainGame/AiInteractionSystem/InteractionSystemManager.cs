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


public enum CharacterEmotions
{
    DEFAULT,
    SAD,
    HAPPY,
    CONFFUSED,
    NEUTRAL,
    ANGRY
}


public enum CharacterStance
{
    DEFAULT,
    CONFUSED,
    ARMRAISED
}


public class InteractionSystemManager : MonoBehaviour
{
    [SerializeField] private GameObject interactionOverlay;
    [SerializeField] private GameObject touchBlockerOverlay;
    [SerializeField] private GameObject speechBubbleImage;
    [SerializeField] private GameObject borrowAmountOverlay;
    [SerializeField] private Button getContactNumBtn;
    [SerializeField] private Button payDebtBtn;
    [SerializeField] private Button borrowMoneyBtn;
    [SerializeField] private Button borrowMoneyOptBtn1;
    [SerializeField] private Button borrowMoneyOptBtn2;
    [SerializeField] private Button borrowMoneyOptBtn3;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI debtValue;
    [SerializeField] private TextMeshProUGUI speechBubbleText;
    [SerializeField] private TextMeshProUGUI relStatusText;
    [SerializeField] private Slider relStatusBar;
    [SerializeField] private Image characterBodyImageObj;
    [SerializeField] private Image characterEmotionImageObj;
    [SerializeField] private Prompts notEnoughMoney;
    private CharactersScriptableObj interactingCharacter;
    private string npcGreetings;
    private ValueTuple<bool, int, string> npcResponse;
    private ValueTuple<CharacterEmotions, CharacterStance, int> npcEmoResponse;
    private float borrowMoneyAmount1, borrowMoneyAmount2, borrowMoneyAmount3;


    public void Interact(CharactersScriptableObj character)
    {
        payDebtBtn.enabled = true;
        getContactNumBtn.enabled = true;
        borrowMoneyBtn.enabled = true;

        interactionOverlay.SetActive(true);
        interactingCharacter = character;
        npcGreetings = interactingCharacter.Interact();

        if (npcGreetings != null)
        {
            StartCoroutine(GreetPlayer());
        }

        if (interactingCharacter.currentDebt == 0f)
        {
            payDebtBtn.enabled = false;
        }
        else
        {
            borrowMoneyBtn.enabled = false;
        }

        if (interactingCharacter.numberObtained)
        {
            getContactNumBtn.enabled = false;
        }

        debtValue.text = "₱" + interactingCharacter.currentDebt.ToString();
        characterNameText.text = interactingCharacter.characterName;
        UpdateRelStatusUI();
    }


    public void EndInteraction()
    {
        StartCoroutine(DoEndInteraction());
    }


    public void Chat()
    {
        npcResponse = interactingCharacter.Chat();
        if (npcResponse.Item1)
        {
            StartCoroutine(ChattingAnim(2f));
        }
        else
        {
            ToggleSpeechBubble(false, npcResponse.Item3);
        }
        UpdateRelStatusUI();
    }


    public void Hug()
    {
        npcResponse = interactingCharacter.Hug();
        if (npcResponse.Item1)
        {
            UpdateCharacterEmo(CharacterEmotions.HAPPY, CharacterStance.DEFAULT);
            UpdateRelStatusUI();
            StartCoroutine(ReturnToDefaultEmo());
        }
        else
        {
            if (npcResponse.Item2 == 0)
            {
                StartCoroutine(DoConsecutiveSpeech(npcResponse.Item3, interactingCharacter.SayBye()));
            }
            else
            {
                ToggleSpeechBubble(false, npcResponse.Item3);
            }
        }
        UpdateRelStatusUI();
    }


    public void SayBye()
    {
        ToggleSpeechBubble(false, interactingCharacter.SayBye());
        EndInteraction();
    }


    public void PayDebt()
    {
        if (Player.Instance.PlayerCash < interactingCharacter.currentDebt)
        {
            PromptManager.Instance.ShowPrompt(notEnoughMoney);
            return;
        }

        interactingCharacter.PayDebt();
        UpdateCharacterEmo(CharacterEmotions.HAPPY, CharacterStance.DEFAULT);
        UpdateRelStatusUI();
        StartCoroutine(ReturnToDefaultEmo());
        payDebtBtn.enabled = false;
        debtValue.text = "₱" + interactingCharacter.currentDebt.ToString();
    }


    public void AskForContactNum()
    {
        npcResponse = interactingCharacter.GetNumber();

        if (npcResponse.Item1)
        {
            Player.Instance.ContactList.Add(interactingCharacter.characterName);
            getContactNumBtn.enabled = false;
        }
        else
        {
            if (npcResponse.Item2 == 0)
            {
                StartCoroutine(DoConsecutiveSpeech(npcResponse.Item3, interactingCharacter.SayBye()));
            }
            else
            {
                ToggleSpeechBubble(false, npcResponse.Item3);
            }
        }

        UpdateRelStatusUI();
    }


    public void YellAt()
    {
        npcEmoResponse = interactingCharacter.YellAt();

        UpdateCharacterEmo(npcEmoResponse.Item1, npcEmoResponse.Item2);
        StartCoroutine(ReturnToDefaultEmo());
        UpdateRelStatusUI();

        if (npcEmoResponse.Item3 == 0)
        {
            SayBye();
        }
    }


    public void TellJoke()
    {
        npcEmoResponse = interactingCharacter.TellJoke();

        if (npcEmoResponse.Item1 == CharacterEmotions.HAPPY)
        {
            StartCoroutine(Laugh());
        }

        UpdateCharacterEmo(npcEmoResponse.Item1, npcEmoResponse.Item2);
        StartCoroutine(ReturnToDefaultEmo());
        UpdateRelStatusUI();

        if (npcEmoResponse.Item3 == 0)
        {
            SayBye();
        }
    }


    public void BorrowMoney()
    {
        npcResponse = interactingCharacter.TryBorrowMoney();

        if (npcResponse.Item1)
        {
            borrowAmountOverlay.SetActive(true);
        }
        else
        {
            if (npcResponse.Item2 == 0)
            {
                StartCoroutine(DoConsecutiveSpeech(npcResponse.Item3, interactingCharacter.SayBye()));
            }
            else
            {
                ToggleSpeechBubble(false, npcResponse.Item3);
            }
        }

        UpdateRelStatusUI();
    }


    public void ProceedBorrowMoney(float amount)
    {
        borrowAmountOverlay.SetActive(false);
        interactingCharacter.currentDebt = amount;
        Player.Instance.PlayerCash += amount;
        Player.Instance.PlayerStatsDict[PlayerStats.MONEY] = Player.Instance.PlayerCash;
        PlayerStatsObserver.onPlayerStatChanged(PlayerStats.MONEY, Player.Instance.PlayerStatsDict);
        debtValue.text = "₱" + interactingCharacter.currentDebt.ToString();
    }


    private void UpdateBorrowMoneyOpts()
    {
        switch (interactingCharacter.relStatus)
        {
            case RelStatus.STRANGERS:
                borrowMoneyAmount1 = 200f;
                borrowMoneyAmount2 = 400f;
                borrowMoneyAmount3 = 500f;
                break;
            case RelStatus.FRIENDS:
                borrowMoneyAmount1 = 600f;
                borrowMoneyAmount2 = 1000f;
                borrowMoneyAmount3 = 1200f;
                break;
            case RelStatus.GOOD_FRIENDS:
                borrowMoneyAmount1 = 1300f;
                borrowMoneyAmount2 = 1500f;
                borrowMoneyAmount3 = 2000f;
                break;
            case RelStatus.BEST_BUDDIES:
                borrowMoneyAmount1 = 2500f;
                borrowMoneyAmount2 = 3500f;
                borrowMoneyAmount3 = 5000f;
                break;
            case RelStatus.ENEMIES:
                borrowMoneyAmount1 = 300f;
                borrowMoneyAmount2 = 500f;
                borrowMoneyAmount3 = 800f;
                break;
            default:
                borrowMoneyAmount1 = 200f;
                borrowMoneyAmount2 = 400f;
                borrowMoneyAmount3 = 500f;
                break;
        }

        borrowMoneyOptBtn1.onClick.RemoveAllListeners();
        borrowMoneyOptBtn2.onClick.RemoveAllListeners();
        borrowMoneyOptBtn3.onClick.RemoveAllListeners();

        borrowMoneyOptBtn1.onClick.AddListener( () => {ProceedBorrowMoney(borrowMoneyAmount1);} );
        borrowMoneyOptBtn2.onClick.AddListener( () => {ProceedBorrowMoney(borrowMoneyAmount2);} );
        borrowMoneyOptBtn3.onClick.AddListener( () => {ProceedBorrowMoney(borrowMoneyAmount3);} );

        borrowMoneyOptBtn1.transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "₱" + borrowMoneyAmount1.ToString();
        borrowMoneyOptBtn2.transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "₱" + borrowMoneyAmount2.ToString();
        borrowMoneyOptBtn3.transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "₱" + borrowMoneyAmount3.ToString();
    }


    private IEnumerator Laugh()
    {
        speechBubbleImage.SetActive(true);
        ToggleSpeechBubble(false, "Hahahah");
        yield return new WaitForSeconds(1.5f);
        ToggleSpeechBubble(true, "");
        yield return null;
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


    private IEnumerator GreetPlayer()
    {
        speechBubbleImage.SetActive(true);
        ToggleSpeechBubble(false, npcGreetings);
        yield return new WaitForSeconds(1.5f);
        ToggleSpeechBubble(true, "");
        yield return null;
    }


    private IEnumerator DoEndInteraction()
    {
        yield return new WaitForSeconds(1f);
        interactingCharacter.OnInteractionEnded();
        interactingCharacter = null;
        ToggleSpeechBubble(true, "");
        interactionOverlay.SetActive(false);
        yield return null;
    }


    private void UpdateRelStatusUI()
    {
        relStatusBar.value = interactingCharacter.relStatBarValue;
        relStatusText.text = interactingCharacter.relStatus.ToString();
        UpdateBorrowMoneyOpts();
    }


    private void ToggleSpeechBubble(bool doHide, string msg)
    {
        if (doHide)
        {
            speechBubbleImage.SetActive(false);
            return;
        }

        speechBubbleImage.SetActive(true);
        speechBubbleText.text = msg;
    }


    private IEnumerator DoConsecutiveSpeech(string msg1, string msg2)
    {
        ToggleSpeechBubble(false, msg1);
        yield return new WaitForSeconds(1f);
        ToggleSpeechBubble(false, msg2);
        yield return null;
    }


    private IEnumerator ReturnToDefaultEmo()
    {
        yield return new WaitForSeconds(2f);
        UpdateCharacterEmo(CharacterEmotions.DEFAULT, CharacterStance.DEFAULT);
        yield return null;
    }


    private void UpdateCharacterEmo(CharacterEmotions emotion, CharacterStance stance)
    {
        switch (emotion)
        {
            case CharacterEmotions.DEFAULT:
                characterEmotionImageObj.sprite = interactingCharacter.defaultEmo;
                break;
            case CharacterEmotions.ANGRY:
                characterEmotionImageObj.sprite = interactingCharacter.angryEmo;
                break;
            case CharacterEmotions.CONFFUSED:
                characterEmotionImageObj.sprite = interactingCharacter.confusedEmo;
                break;
            case CharacterEmotions.HAPPY:
                characterEmotionImageObj.sprite = interactingCharacter.happyEmo;
                break;
            case CharacterEmotions.NEUTRAL:
                characterEmotionImageObj.sprite = interactingCharacter.neutralEmo;
                break;
            case CharacterEmotions.SAD:
                characterEmotionImageObj.sprite = interactingCharacter.sadEmo;
                break;
            default:
                characterEmotionImageObj.sprite = interactingCharacter.defaultEmo;
                break;
        }

        switch (stance)
        {
            case CharacterStance.DEFAULT:
                characterBodyImageObj.sprite = interactingCharacter.defaultBody;
                break;
            case CharacterStance.CONFUSED:
                characterBodyImageObj.sprite = interactingCharacter.confusedBody;
                break;
            case CharacterStance.ARMRAISED:
                characterBodyImageObj.sprite = interactingCharacter.armRaisedBody;
                break;
            default:
                characterBodyImageObj.sprite = interactingCharacter.defaultBody;
                break;
        }
    }
}
