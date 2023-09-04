using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject exitBtn;
    [SerializeField] private GameObject settingsBtn;


    private void Start()
    {
        AnimateMainMenu();
    }


    private void AnimateMainMenu()
    {
        LeanTween.moveY(startBtn, (float)(Screen.height / 3.5), 1.2f).setEaseOutElastic().delay = 0.2f;
        LeanTween.moveY(exitBtn, (float)(Screen.height / 8.5), 1.2f).setEaseOutElastic().setOnComplete(StartSpawn).delay = 0.4f;
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


    public void ExitGame()
    {
        Application.Quit();
    }
}
