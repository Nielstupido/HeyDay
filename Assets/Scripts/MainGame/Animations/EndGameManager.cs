using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private GameObject outroOverlay;
    [SerializeField] private GameObject badEndOverlay;
    [SerializeField] private GameObject goodEndOverlay;
    [SerializeField] private Image sceneImageObj;
    [SerializeField] private RectTransform topBar;
    [SerializeField] private RectTransform botBar;
    [SerializeField] private List<Sprite> goodSceneImagesGirl = new List<Sprite>();
    [SerializeField] private List<Sprite> goodSceneImagesBoy = new List<Sprite>();
    [SerializeField] private List<Sprite> badSceneImagesGirl = new List<Sprite>();
    [SerializeField] private List<Sprite> badSceneImagesBoy = new List<Sprite>();
    private bool isGoodEnding;


    private IEnumerator StartCutscene(List<Sprite> sceneImages)
    {
        for(int i = 1; i < sceneImages.Count; i++)
        {
            yield return new WaitForSeconds(2.5f);
            AnimOverlayManager.Instance.StartBlackScreenFadeLoadScreen();
            yield return new WaitForSeconds(0.5f);
            sceneImageObj.sprite = sceneImages[i];
        }

        yield return new WaitForSeconds(4f);
        AnimOverlayManager.Instance.StartScreenFadeLoadScreen();
        yield return new WaitForSeconds(0.7f);
        ReturnHome();
        yield return null;
    }


    public void ShowOutro(bool isGameFinished)
    {
        isGoodEnding = isGameFinished;
        
        if (isGoodEnding)
        {
            goodEndOverlay.SetActive(true); 
        }
        else
        {
            badEndOverlay.SetActive(true); 
        }
    }


    public void StartOutro()
    {
        StartCoroutine(ContinueOutro());
    }


    private IEnumerator ContinueOutro()
    {
        AnimOverlayManager.Instance.StartScreenFadeLoadScreen();
        yield return new WaitForSeconds(0.5f);
        if (isGoodEnding)
        {
            if (Player.Instance.PlayerGender == Gender.MALE)
            {
                sceneImageObj.sprite = goodSceneImagesBoy[0];
                outroOverlay.SetActive(true);
                botBar.LeanSize(new Vector2(0f, 90f), 2f).delay = 0.5f;
                topBar.LeanSize(new Vector2(0f, 90f), 2f).setOnComplete( () => {StartCoroutine(StartCutscene(goodSceneImagesBoy));} ).delay = 0.5f;
            }
            else
            {
                sceneImageObj.sprite = goodSceneImagesGirl[0];
                outroOverlay.SetActive(true);
                botBar.LeanSize(new Vector2(0f, 90f), 2f).delay = 0.5f;
                topBar.LeanSize(new Vector2(0f, 90f), 2f).setOnComplete( () => {StartCoroutine(StartCutscene(goodSceneImagesGirl));} ).delay = 0.5f;
            }
        }
        else
        {
            if (Player.Instance.PlayerGender == Gender.MALE)
            {
                sceneImageObj.sprite = badSceneImagesBoy[0];
                outroOverlay.SetActive(true);
                botBar.LeanSize(new Vector2(0f, 90f), 2f).delay = 0.5f;
                topBar.LeanSize(new Vector2(0f, 90f), 2f).setOnComplete( () => {StartCoroutine(StartCutscene(badSceneImagesBoy));} ).delay = 0.5f;
            }
            else
            {
                sceneImageObj.sprite = badSceneImagesGirl[0];
                outroOverlay.SetActive(true);
                botBar.LeanSize(new Vector2(0f, 90f), 2f).delay = 0.5f;
                topBar.LeanSize(new Vector2(0f, 90f), 2f).setOnComplete( () => {StartCoroutine(StartCutscene(badSceneImagesGirl));} ).delay = 0.5f;
            }
        }
        yield return null;
    }


    public void ReturnHome()
    {
        SceneManager.LoadScene("MainMenu");
        GameDataManager.Instance.AllPlayersGameStateData.Remove(Player.Instance.PlayerName);
        GameDataManager.Instance.SaveGameStateData();
    }
}
