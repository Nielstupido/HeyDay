using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject exitBtn;
    [SerializeField] private GameObject dictionaryBtn;
    [SerializeField] private GameObject dictionaryOverlay;
    [SerializeField] private GameObject settingsBtn;
    [SerializeField] private GameObject mechanicsBtn;
    [SerializeField] private GameObject creditsBtn;
    [SerializeField] private GameObject mechanicsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject closeBtn;


    private void Start()
    {
        AnimateMainMenu();
    }


    private void AnimateMainMenu()
    {
        LeanTween.moveX(exitBtn, (float)(Screen.width / 5), 1.2f).setEaseOutElastic().delay = 0.15f;
        LeanTween.moveX(creditsBtn, (float)(Screen.width / 5), 1.2f).setEaseOutElastic().delay = 0.2f;
        LeanTween.moveX(dictionaryBtn, (float)(Screen.width / 5), 1.2f).setEaseOutElastic().delay = 0.25f;
        LeanTween.moveX(mechanicsBtn, (float)(Screen.width / 5), 1.2f).setEaseOutElastic().delay = 0.3f;
        LeanTween.moveX(startBtn, (float)(Screen.width / 5), 1.2f).setEaseOutElastic().setOnComplete(StartSpawn).delay = 0.4f;
    }


    private void StartSpawn()
    {
        MenuObjManager.onBtnSet();
    }


    public void StartGame()
    {
        MenuObjManager.onGameStart();
        SceneManager.LoadScene("MainGame");
    }


    public void ShowGameMechanics()
    {
        mechanicsPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("Select");
    }


    public void ShowGameCredits()
    {
        creditsPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("Select");
    }


    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("Select");
    }


    public void ShowGameDictionary()
    {
        dictionaryOverlay.SetActive(true);
    }


    public void ExitGameDictionary()
    {
        dictionaryOverlay.SetActive(false);
    }
    

    public void ExitSettings()
    {
        settingsPanel.SetActive(false);
        //AudioManager.Instance.PlaySFX("Select");
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
