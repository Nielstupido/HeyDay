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
    private string npcContactName;


    public void SetupContactItem(string contactName, PlayerPhone playerPhoneRef)
    {
        this.contactNameText.text = contactName;
        this.npcContactName = contactName;
        this.playerPhone = playerPhoneRef;
        this.contactBtn.onClick.AddListener( () => {CallContact(contactName);});
    }


    public void CallContact(string name)
    {
        Debugger.Instance.ShowError(name);

        if (MeetUpSystem.Instance.CheckForPendingMeetup())
        {
            PromptManager.Instance.ShowPrompt(pendingMeetupPrompt);
            return;
        }

        this.playerPhone.DialContact(name);
    }
}
