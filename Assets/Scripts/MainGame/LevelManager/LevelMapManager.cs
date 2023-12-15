using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMapManager : MonoBehaviour
{
    [SerializeField] private GameObject levelMapCanvas;
    [SerializeField] private GameObject nextLvlBtn;
    [SerializeField] private Transform characterImage;
    [SerializeField] private GameObject map1;
    [SerializeField] private GameObject map2;
    [SerializeField] private GameObject map3;
    [SerializeField] private GameObject map4;
    [SerializeField] private GameObject endLvlOverlay;
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject badgeAward;
    [SerializeField] private GameObject budgetTracker;
    [SerializeField] private List<Transform> mapPlaceholders1 = new List<Transform>();
    [SerializeField] private List<Transform> mapPlaceholders2 = new List<Transform>();
    [SerializeField] private List<Transform> mapPlaceholders3 = new List<Transform>();
    [SerializeField] private List<Transform> mapPlaceholders4 = new List<Transform>();
    private bool isNewMap;
    public static LevelMapManager Instance { get; private set; }


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


    public void MoveCharacterPosition(bool isSceneSetup, bool startNewMap, int lvlNum)
    {
        if (lvlNum < 11)
        {
            map1.SetActive(true);
            characterImage.SetParent(mapPlaceholders1[lvlNum - 1]);
        }
        else if (lvlNum < 21)
        {
            if (lvlNum == 11 && !isSceneSetup && !startNewMap)
            {
                isNewMap = true;
                map1.SetActive(true);
            }
            else
            {
                map2.SetActive(true);
            }
            
            characterImage.SetParent(mapPlaceholders2[lvlNum - 1]);
        }
        else if (lvlNum < 31)
        {
            if (lvlNum == 21 && !isSceneSetup && !startNewMap)
            {
                isNewMap = true;
                map2.SetActive(true);
            }
            else
            {
                map3.SetActive(true);
            }
            
            characterImage.SetParent(mapPlaceholders3[lvlNum - 1]);
        }
        else
        {
            if (lvlNum == 31 && !isSceneSetup && !startNewMap)
            {
                isNewMap = true;
                map3.SetActive(true);
            }
            else
            {
                map4.SetActive(true);
            }

            characterImage.SetParent(mapPlaceholders4[lvlNum - 1]);
        }

        if (isSceneSetup)
        {
            characterImage.GetComponent<RectTransform>().localPosition = Vector3.zero;
            return;
        }

        levelMapCanvas.SetActive(true);
        StartCoroutine(LerpCharacter(lvlNum));
    }


    public void MoveToNewLevel(int nextLvl, bool startNewMap)
    {
        AnimOverlayManager.Instance.StartScreenFadeLoadScreen();

        isNewMap = false;
        nextLvlBtn.SetActive(false);
        map1.SetActive(false);
        map2.SetActive(false);
        map3.SetActive(false);
        map4.SetActive(false);
        MoveCharacterPosition(false, startNewMap, nextLvl);
    }


    public void StartNextLevel()
    {
        AudioManager.Instance.PlaySFX("Select");
        AnimOverlayManager.Instance.StartWhiteScreenFadeLoadScreen();
        levelMapCanvas.SetActive(false);
        endLvlOverlay.SetActive(false);
        leaderboard.SetActive(false);
        badgeAward.SetActive(false);
        budgetTracker.SetActive(false);
        Player.Instance.ResetLvlExpenses();
        GameManager.Instance.StartLevel();
    }


    public void StartNewMap(int lvlNum)
    {
        MoveToNewLevel(lvlNum, true);
    }


    private IEnumerator LerpCharacter(int lvlNum)
    {
        characterImage.gameObject.GetComponent<Image>().sprite = Player.Instance.CurrentCharacter.defaultBody;
        yield return new WaitForSeconds(1f);
        float timeCounter = 0;
        float speed = 20f;

        while (timeCounter < speed)
        {
            if (Vector3.Distance(characterImage.GetComponent<RectTransform>().localPosition, Vector3.zero) < 2f)
            {
                break;
            }

            characterImage.GetComponent<RectTransform>().localPosition = Vector3.Lerp(characterImage.GetComponent<RectTransform>().localPosition, Vector3.zero, timeCounter / speed);
            timeCounter += Time.deltaTime;
            yield return null;
        }

        characterImage.GetComponent<RectTransform>().localPosition = Vector3.zero;

        if (isNewMap)
        {
            StartNewMap(lvlNum);
        }
        else
        {
            nextLvlBtn.SetActive(true);
        }
    }
}
