using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromptManager : MonoBehaviour
{
    [SerializeField] private GameObject promptOverlay;
    [SerializeField] private GameObject promptPopUp;
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private TextMeshProUGUI title;
    public static PromptManager Instance {private set; get;}


    private void Awake() 
    { 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void ShowPrompt(Prompts prompt)
    {
        promptOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(promptPopUp);
        title.text = prompt.promptTitle;
        content.text = prompt.promptContent;
    }

    public void HidePrompt()
    {
        AudioManager.Instance.PlaySFX("Select");
        OverlayAnimations.Instance.AnimCloseOverlay(promptPopUp, promptOverlay);
    }
}
