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
        contactNameText.text = contactName;
        contactBtn.onClick.AddListener( () => {CallContact(contactName);} );
        playerPhone = playerPhoneRef;
    }


    public void CallContact(string contactName)
    {
        if (MeetUpSystem.Instance.CheckForPendingMeetup())
        {
            PromptManager.Instance.ShowPrompt(pendingMeetupPrompt);
            return;
        }

        playerPhone.DialContact(contactName);
    }
}
