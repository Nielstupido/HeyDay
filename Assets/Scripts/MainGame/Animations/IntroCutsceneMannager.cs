using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IntroCutsceneMannager : MonoBehaviour
{
    [SerializeField] private GameObject IntroOverlay;
    [SerializeField] private Image sceneImageObj;
    [SerializeField] private RectTransform topBar;
    [SerializeField] private RectTransform botBar;
    [SerializeField] private Sprite firstScene;
    [SerializeField] private GoalSetter goalSetter;
    [SerializeField] private List<Sprite> sceneImagesGirl = new List<Sprite>();
    [SerializeField] private List<Sprite> sceneImagesBoy = new List<Sprite>();


    private IEnumerator StartCutscene()
    {
        if (Player.Instance.PlayerGender == Gender.MALE)
        {
            foreach(Sprite image in sceneImagesBoy)
            {
                yield return new WaitForSeconds(2.5f);
                AnimOverlayManager.Instance.StartBlackScreenFadeLoadScreen();
                yield return new WaitForSeconds(0.5f);
                sceneImageObj.sprite = image;
            }
        }
        else
        {
            foreach(Sprite image in sceneImagesGirl)
            {
                yield return new WaitForSeconds(2.5f);
                AnimOverlayManager.Instance.StartBlackScreenFadeLoadScreen();
                yield return new WaitForSeconds(0.5f);
                sceneImageObj.sprite = image;
            }
        }

        goalSetter.SetGoal();
        yield return new WaitForSeconds(2.5f);
        AnimOverlayManager.Instance.StartBlackScreenFadeLoadScreen();
        yield return new WaitForSeconds(0.5f);
        IntroOverlay.SetActive(false);
        yield return null;
    }


    public void StartIntro()
    {
        sceneImageObj.sprite = firstScene;
        IntroOverlay.SetActive(true);
        botBar.LeanSize(new Vector2(0f, 90f), 2f).delay = 0.5f;
        topBar.LeanSize(new Vector2(0f, 90f), 2f).setOnComplete( () => {StartCoroutine(StartCutscene());} ).delay = 0.5f;
    }
}
