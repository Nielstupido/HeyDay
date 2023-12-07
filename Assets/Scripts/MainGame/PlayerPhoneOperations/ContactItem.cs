using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ContactItem : MonoBehaviour
{
    [SerializeField] private Button contactBtn;
    [SerializeField] private TextMeshProUGUI contactNameText;
    private PlayerPhone playerPhone;


    public void SetupContactItem(string contactName, PlayerPhone playerPhoneRef)
    {
        contactNameText.text = contactName;
        contactBtn.onClick.AddListener( () => {CallContact(contactName);} );
        playerPhone = playerPhoneRef;
    }


    public void CallContact(string contactName)
    {
        playerPhone.DialContact(contactName);
    }
}
