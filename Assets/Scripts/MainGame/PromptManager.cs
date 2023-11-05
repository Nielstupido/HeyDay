using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromptManager : MonoBehaviour
{
    [SerializeField] private GameObject promptOverlay;
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private TextMeshProUGUI title;
    public static PromptManager Instance {private set; get;}


    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }


    public void ShowPrompt(Prompts prompt)
    {
        promptOverlay.SetActive(true);
        title.text = prompt.promptTitle;
        content.text = prompt.promptContent;
    }
}
