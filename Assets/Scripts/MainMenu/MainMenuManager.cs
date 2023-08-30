using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject exitBtn;
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
        LeanTween.moveY(startBtn, (float)(Screen.height / 3.5), 1.2f).setEaseOutElastic().delay = 0.2f;
        LeanTween.moveY(exitBtn, (float)(Screen.height / 8.5), 1.2f).setEaseOutElastic().setOnComplete(StartSpawn).delay = 0.4f;
        LeanTween.moveY(mechanicsBtn, (float)(Screen.height / 4.5), 1.2f).setEaseOutElastic().setOnComplete(StartSpawn).delay = 0.2f;
        LeanTween.moveY(creditsBtn, (float)(Screen.height / 4.5), 1.2f).setEaseOutElastic().setOnComplete(StartSpawn).delay = 0.2f;
    }


    private void StartSpawn()
    {
        MenuObjManager.onBtnSet();
    }


    //public methods
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
