using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PlayerPhone : MonoBehaviour
{
    private string chosenCourse = "BS Information Technology"; //Temporary variable
    private float totalSavings = 100; //Temporary variable - Total Bank Savings
    private float totalEmergencyFunds = 1000; //Temporary variable
    private float totalCash = 5000; //Temporary variable
    private float currentIncomePerHour = 20; //Temporary variable
    private float taxRate = 0.5f; //Temporary variable
    private float totalDebt = 200; //Temporary variable
    private float outflowPerMonth; //Temporary variable - Rent + Water & Elec Bill
    private float rentRate = 1500; //Temporary variable
    private float waterBillRate = 200; //Temporary variable
    private float electricityBillRate = 250; //Temporary variable
    private float groceryPrice = 200; //Temporary variable
    
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

    [SerializeField] private TextMeshProUGUI courseValue;
    [SerializeField] private TextMeshProUGUI savingsValue;
    [SerializeField] private TextMeshProUGUI bankBalValue;
    [SerializeField] private TextMeshProUGUI emergencyFundsValue;
    [SerializeField] private TextMeshProUGUI cashBalValue;
    [SerializeField] private TextMeshProUGUI incomeValue;
    [SerializeField] private TextMeshProUGUI taxValue;
    [SerializeField] private TextMeshProUGUI debtValue;
    [SerializeField] private TextMeshProUGUI monthlyOutflowValue;


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
        courseValue.text = chosenCourse;
        savingsValue.text = "₱" + totalSavings.ToString() + "/100000";
        LevelManager.onFinishedPlayerAction(MissionType.USEAPP, interactedApp:APPS.GOALTRACKER);
    }


    public void FinanceTracker()
    {
        financeTrackerOverlay.SetActive(true);
        bankBalValue.text = "₱" + totalSavings.ToString();
        emergencyFundsValue.text = "₱" + totalEmergencyFunds.ToString();
        cashBalValue.text = "₱" + totalCash.ToString();
        incomeValue.text = currentIncomePerHour.ToString() + "/hr";
        taxValue.text = taxRate.ToString() + "%";
        debtValue.text = "₱" + totalDebt.ToString();
        monthlyOutflowValue.text = "₱" + (rentRate + waterBillRate + electricityBillRate).ToString();
        LevelManager.onFinishedPlayerAction(MissionType.USEAPP, interactedApp:APPS.FINANCETRACKER);
    }


    public void OLShop()
    {
        oLShopOverlay.SetActive(true);
        groceryPriceValue.text = "₱" + groceryPrice.ToString();
        LevelManager.onFinishedPlayerAction(MissionType.USEAPP, interactedApp:APPS.OLSHOP);
    }


    public void BuyGrocery()
    {
        groceryBar.value += 10;
        // Player.Instance.Purchase(false, 200f);
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
