using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeEventsPrompt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messagePrompt;
    [SerializeField] private GameObject messagePromptOverlay;
    public static LifeEventsPrompt Instance { get; private set; }


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


    public void DisplayPrompt(string message)
    {
        AudioManager.Instance.StopMusic();
        messagePrompt.text = message;
        messagePromptOverlay.SetActive(true);
    }

    public void Continue()
    {
        AudioManager.Instance.StopSFX();
        AudioManager.Instance.PlaySFX("Select");
        messagePromptOverlay.SetActive(false);
    }
}
