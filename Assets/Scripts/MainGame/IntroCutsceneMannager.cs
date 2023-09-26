using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IntroCutsceneMannager : MonoBehaviour
{
    [SerializeField] private List<Sprite> sceneImagesGirl = new List<Sprite>();
    [SerializeField] private List<Sprite> sceneImagesBoy = new List<Sprite>();
    private Image sceneImageObj;


    private void Start()
    {
        sceneImageObj = gameObject.GetComponent<Image>();
    }


    private IEnumerator StartCutscene()
    {
        if (Player.Instance.PlayerGender == Gender.MALE)
        {
            foreach(Sprite image in sceneImagesBoy)
            {
                yield return new WaitForSeconds(2.5f);
                sceneImageObj.sprite = image;
            }
        }
        else
        {
            foreach(Sprite image in sceneImagesGirl)
            {
                yield return new WaitForSeconds(2.5f);
                sceneImageObj.sprite = image;
            }
        }

        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        GameManager.Instance.StartGame();
    }


    public void StartIntro()
    {
        gameObject.SetActive(true);
        StartCoroutine(StartCutscene());
    }
}
