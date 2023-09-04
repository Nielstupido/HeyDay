using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject exitBtn;
    [SerializeField] private GameObject settingsBtn;
    [SerializeField] private GameObject dictionaryBtn;
    [SerializeField] private GameObject dictionaryOverlay;


    private void Start()
    {
        AnimateMainMenu();
    }


    private void AnimateMainMenu()
    {
        LeanTween.moveY(startBtn, (float)(Screen.height / 2.7), 1.2f).setEaseOutElastic().delay = 0.2f;
        LeanTween.moveY(dictionaryBtn, (float)(Screen.height / 4.4), 1.2f).setEaseOutElastic().setOnComplete(StartSpawn).delay = 0.4f;
        LeanTween.moveY(exitBtn, (float)(Screen.height / 8), 1.2f).setEaseOutElastic().setOnComplete(StartSpawn).delay = 0.4f;
        
    
    
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

    public void ShowGameDictionary()
    {
        dictionaryOverlay.SetActive(true);
    }


    public void ExitGameDictionary()
    {
        dictionaryOverlay.SetActive(false);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
