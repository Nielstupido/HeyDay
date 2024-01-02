using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ContactItem : MonoBehaviour
{
    [SerializeField] private Button contactBtn;
    [SerializeField] private TextMeshProUGUI contactNameText;
    [SerializeField] private Prompts pendingMeetupPrompt;
    private PlayerPhone playerPhone;


    public void SetupContactItem(string contactName, PlayerPhone playerPhoneRef)
    {
        this.contactNameText.text = contactName;
        this.playerPhone = playerPhoneRef;
        this.contactBtn.onClick.AddListener( () => {this.CallContact();});
    }


    public void CallContact()
    {
        if (MeetUpSystem.Instance.CheckForPendingMeetup())
        {
            PromptManager.Instance.ShowPrompt(pendingMeetupPrompt);
            return;
        }

        Debug.Log("dial " + this.contactNameText.text);
        this.playerPhone.DialContact(this.contactNameText.text);
    }
}
