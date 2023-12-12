using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PlayerPhone : MonoBehaviour
{
    [SerializeField] private GameObject phoneOverlay;
    [SerializeField] private MeetUpSystem meetingUpSystem;

    //Phonebook
    [SerializeField] private GameObject phoneBookOverlay;
    [SerializeField] private Transform contactListHolder;
    [SerializeField] private GameObject contactItemPrefab;
    [SerializeField] private GameObject noContactsText;
    [SerializeField] private GameObject callConvoView;
    [SerializeField] private GameObject inviteBtn;
    [SerializeField] private Prompts contactBusyPrompt;
    [SerializeField] private GameObject playerBubbleHolder;
    [SerializeField] private GameObject npcBubbleHolder;
    [SerializeField] private TextMeshProUGUI playerMsgText;
    [SerializeField] private TextMeshProUGUI npcResponseText;
    private string currrentSelectedContact;
    
    //Goaltracker
    [SerializeField] private GameObject goalTrackerOverlay;

    //Financetracker
    [SerializeField] private GameObject financeTrackerOverlay;

    //Olshop
    [SerializeField] private GameObject oLShopOverlay;
    [SerializeField] private TextMeshProUGUI groceryPriceValue;
    [SerializeField] private Slider groceryBar;
    private float groceryPrice = 200f;

    [SerializeField] private TextMeshProUGUI courseValue;
    [SerializeField] private TextMeshProUGUI savingsValue;
    [SerializeField] private TextMeshProUGUI bankBalValue;
    [SerializeField] private TextMeshProUGUI cashBalValue;
    [SerializeField] private TextMeshProUGUI incomeValue;
    [SerializeField] private TextMeshProUGUI debtValue; 
    [SerializeField] private TextMeshProUGUI monthlyOutflowValue; 
    [SerializeField] private Prompts noGroceryPrompt; 
    [SerializeField] private Prompts errorMoneyPrompt; 
    [SerializeField] private Prompts groceryBarFull; 


    public void OpenPhone()
    {
        phoneOverlay.SetActive(true);
        GameUiController.onScreenOverlayChanged(UIactions.SHOW_SMALL_BOTTOM_OVERLAY);
        playerMsgText.text = "";
        npcResponseText.text = "";
        playerMsgText.gameObject.SetActive(false);
        npcResponseText.gameObject.SetActive(false);
    }


    public void ClosePhone()
    {
        phoneOverlay.SetActive(false);
        GameUiController.onScreenOverlayChanged(UIactions.SHOW_DEFAULT_BOTTOM_OVERLAY);
    }


    public void GoalTracker()
    {
        goalTrackerOverlay.SetActive(true);
                
        try
        {
            courseValue.text = GameManager.Instance.EnumStringParser(Player.Instance.PlayerEnrolledCourse);
        }
        catch (System.Exception)
        {
            
            courseValue.text = "Currently not enrolled to any course.";
        }

        savingsValue.text = "₱" + Player.Instance.PlayerBankSavings.ToString() + " / 100000";
        LevelManager.onFinishedPlayerAction(MissionType.USEAPP, interactedApp:APPS.GOALTRACKER);
    }


    public void FinanceTracker()
    {
        financeTrackerOverlay.SetActive(true);
        bankBalValue.text = Player.Instance.PlayerBankSavings.ToString();
        cashBalValue.text = Player.Instance.PlayerCash.ToString();

        try
        {
            incomeValue.text = Player.Instance.CurrentPlayerJob.salaryPerHr.ToString() + "/hr";
        }
        catch (System.Exception)
        {
            
            incomeValue.text = "0/hr";
        }
        
        try
        {
            monthlyOutflowValue.text = (Player.Instance.CurrentPlayerPlace.monthlyRent + Player.Instance.CurrentPlayerPlace.monthlyWaterCharge + 
                                        Player.Instance.CurrentPlayerPlace.monthlyElecCharge).ToString();
        }
        catch (System.Exception)
        {
            
            monthlyOutflowValue.text = "0";
        }

        debtValue.text = Player.Instance.PlayerTotalDebt.ToString();
        LevelManager.onFinishedPlayerAction(MissionType.USEAPP, interactedApp:APPS.FINANCETRACKER);
    }


    public void OLShop()
    {
        oLShopOverlay.SetActive(true);
        // groceryPriceValue.text = "₱" + groceryPrice.ToString();
        groceryBar.value = Player.Instance.GroceryBarValue;
        LevelManager.onFinishedPlayerAction(MissionType.USEAPP, interactedApp:APPS.OLSHOP);
    }


    public void BuyGrocery()
    {
        if (Player.Instance.GroceryBarValue < 10)
        {
            if (groceryPrice > Player.Instance.PlayerCash)
            {
                PromptManager.Instance.ShowPrompt(errorMoneyPrompt);
                return;
            }

            StartCoroutine(DoAnim(ActionAnimations.BUY, 2f));

            TimeManager.Instance.AddClockTime(0.1f);
            Player.Instance.PlayerCash -= groceryPrice;
            Player.Instance.PlayerStatsDict[PlayerStats.MONEY] -= groceryPrice;
            PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, Player.Instance.PlayerStatsDict);

            groceryBar.value++;
            Player.Instance.GroceryBarValue++;

            return;
        }

        PromptManager.Instance.ShowPrompt(groceryBarFull);
    }


    public void ConsumeGrocery()
    {
        if (Player.Instance.GroceryBarValue > 0)
        {
            StartCoroutine(DoAnim(ActionAnimations.EAT, 2f));
            TimeManager.Instance.AddClockTime(0.2f);
            Player.Instance.PlayerStatsDict[PlayerStats.HAPPINESS] += 15f;
            Player.Instance.PlayerStatsDict[PlayerStats.ENERGY] += 25f;
            Player.Instance.PlayerStatsDict[PlayerStats.HUNGER] += 20f;

            PlayerStatsObserver.onPlayerStatChanged(PlayerStats.ALL, Player.Instance.PlayerStatsDict);

            Player.Instance.GroceryBarValue++;
            groceryBar.value++;
            return;
        }
        
        PromptManager.Instance.ShowPrompt(noGroceryPrompt);
    }


    private IEnumerator DoAnim(ActionAnimations actionAnimation, float animLength)
    {
        AnimOverlayManager.Instance.StartAnim(actionAnimation);
        yield return new WaitForSeconds(animLength);
        AnimOverlayManager.Instance.StopAnim();
        yield return null;
    }

    //<<<<<<<< Phonebook >>>>>>>
    public void PhoneBook()
    {
        noContactsText.SetActive(false);
        phoneBookOverlay.SetActive(true); 
        LevelManager.onFinishedPlayerAction(MissionType.USEAPP, interactedApp:APPS.PHONEBOOK);
        PrepareContactList();
    }


    public void DialContact(string characterName)
    {
        if (GameManager.Instance.Characters[GameManager.Instance.Characters.FindIndex( (character) => character.name == currrentSelectedContact )].gotCalledToday)
        {
            PromptManager.Instance.ShowPrompt(contactBusyPrompt);
            return;
        }

        currrentSelectedContact = characterName;
        StartCoroutine(Dialing());
    }


    public void InviteMeetup()
    {
        playerMsgText.text = "Hi " + currrentSelectedContact.Split(' ').First() + "! It's been a minute. Wanna hang out?";
        playerBubbleHolder.SetActive(true);
        StartCoroutine(GettingNpcResponse());
        GameManager.Instance.Characters[GameManager.Instance.Characters.FindIndex( (character) => character.name == currrentSelectedContact )].gotCalledToday = true;
        inviteBtn.SetActive(false);
    }


    private void GetNpcResponse()
    {
        npcResponseText.text = MeetUpSystem.Instance.InviteNpcMeetup(currrentSelectedContact);
        npcBubbleHolder.SetActive(true);
        StartCoroutine(CloseCallConvoView());
    }


    private IEnumerator CloseCallConvoView()
    {
        yield return new WaitForSeconds(1.5f);
        callConvoView.SetActive(false);
        yield return null;
    }


    private IEnumerator GettingNpcResponse()
    {
        yield return new WaitForSeconds(0.5f);
        GetNpcResponse();
        yield return null;
    }


    private IEnumerator Dialing()
    {
        AnimOverlayManager.Instance.StartAnim(ActionAnimations.DIAL);
        yield return new WaitForSeconds(1.5f);
        AnimOverlayManager.Instance.StopAnim();
        callConvoView.SetActive(true);
        inviteBtn.SetActive(true);
        yield return null;
    }

    
    private void PrepareContactList()
    {
        RemoveContacts();
        if (Player.Instance.ContactList.Count == 0)
        {
            noContactsText.SetActive(true);
            return;
        }

        foreach(string characterName in Player.Instance.ContactList)
        {
            GameObject newNpc = Instantiate(contactItemPrefab, Vector3.zero, Quaternion.identity, contactListHolder);
            newNpc.GetComponent<ContactItem>().SetupContactItem(characterName, this);
        }
    } 


    private void RemoveContacts()
    {
        for (var i = 0; i < contactListHolder.childCount; i++)
        {
            Object.Destroy(contactListHolder.GetChild(i).gameObject);
        }
    }
    //<<<<<<<< Phonebook >>>>>>>
}
