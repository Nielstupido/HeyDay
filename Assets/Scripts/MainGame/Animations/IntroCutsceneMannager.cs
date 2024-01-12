using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class IntroCutsceneMannager : MonoBehaviour
{
    [SerializeField] private GameObject IntroOverlay;
    [SerializeField] private GameObject skipBtn;
    [SerializeField] private Image sceneImageObj;
    [SerializeField] private TextMeshProUGUI subText;
    [SerializeField] private RectTransform topBar;
    [SerializeField] private RectTransform botBar;
    [SerializeField] private Sprite firstScene;
    [SerializeField] private List<Sprite> sceneImagesGirl = new List<Sprite>();
    [SerializeField] private List<Sprite> sceneImagesBoy = new List<Sprite>();
    [SerializeField] private List<string> sceneSubtitles = new List<string>();


    private IEnumerator StartCutscene()
    {
        AudioManager.Instance.PlayMusic("Intro");
        int i = 1;
        string tempString;
        skipBtn.SetActive(true);

        if (Player.Instance.PlayerGender == Gender.MALE)
        {
            foreach(Sprite image in sceneImagesBoy)
            {
                yield return new WaitForSeconds(5f);
                AnimOverlayManager.Instance.StartBlackScreenFadeLoadScreen();
                yield return new WaitForSeconds(0.5f);
                tempString = sceneSubtitles[i].Replace("X", Player.Instance.PlayerName);
                subText.text = tempString.Replace("\\n", "\n");
                sceneImageObj.sprite = image;
                i++;
            }
        }
        else
        {
            foreach(Sprite image in sceneImagesGirl)
            {
                yield return new WaitForSeconds(5f);
                AnimOverlayManager.Instance.StartBlackScreenFadeLoadScreen();
                yield return new WaitForSeconds(0.5f);
                tempString = sceneSubtitles[i].Replace("X", Player.Instance.PlayerName);
                subText.text = tempString.Replace("\\n", "\n");
                sceneImageObj.sprite = image;
                i++;
            }
        }

        yield return new WaitForSeconds(2.5f);
        AnimOverlayManager.Instance.StartBlackScreenFadeLoadScreen();
        yield return new WaitForSeconds(0.5f);
        IntroOverlay.SetActive(false);
        GoalSetter.Instance.SetGoal();
        AudioManager.Instance.StopMusic();
        yield return null;
    }


    public void StartIntro()
    {
        subText.text = sceneSubtitles[0];
        sceneImageObj.sprite = firstScene;
        IntroOverlay.SetActive(true);
        botBar.LeanSize(new Vector2(0f, 90f), 2f).delay = 0.5f;
        topBar.LeanSize(new Vector2(0f, 90f), 2f).setOnComplete( () => {StartCoroutine(StartCutscene());} ).delay = 0.5f;
    }


    public void SkipIntro()
    {
        StopCoroutine(StartCutscene());
        StartCoroutine(SkipCutscenes());
    }


    private IEnumerator SkipCutscenes()
    {
        AnimOverlayManager.Instance.StartBlackScreenFadeLoadScreen();
        yield return new WaitForSeconds(0.5f);
        IntroOverlay.SetActive(false);
        GoalSetter.Instance.SetGoal();
        AudioManager.Instance.StopMusic();
        yield return null;
    }
}
