using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMapManager : MonoBehaviour
{
    [SerializeField] private GameObject levelMapCanvas;
    [SerializeField] private GameObject nextLvlBtn;
    [SerializeField] private Image characterImage;
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
    private Vector3 targetPos;
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


    private int GetIndexValue(int num, bool startNewMap)
    {
        if (!startNewMap)
        {
            switch (num)
            {
                case 11: 
                    return 11;
                case 21: 
                    return 11;
                case 31: 
                    return 11;
            }
        }
        
        switch (num)
        {
            case 11: case 21: case 31:
                return 1;
            case 12: case 22: case 32:
                return 2;
            case 13: case 23: case 33:
                return 3;
            case 14: case 24: case 34:
                return 4;
            case 15: case 25: case 35:
                return 5;
            case 16: case 26: case 36:
                return 6;
            case 17: case 27: case 37:
                return 7;
            case 18: case 28: case 38:
                return 8;
            case 19: case 29: case 39:
                return 9;
            case 20: case 30: case 40:
                return 10;
            default: return (num);
        }
    }


    public void MoveCharacterPosition(bool isSceneSetup, bool startNewMap, int lvlNum)
    {
        if (lvlNum < 11)
        {
            characterImage.transform.SetParent(map1.transform);
            map1.SetActive(true);
            targetPos = mapPlaceholders1[GetIndexValue(lvlNum, startNewMap) - 1].localPosition;
            // characterImage.transform.SetParent(mapPlaceholders1[GetIndexValue(lvlNum, startNewMap) - 1]);
        }
        else if (lvlNum < 21)
        {
            if (lvlNum == 11 && !isSceneSetup && !startNewMap)
            {
                isNewMap = true;
                map1.SetActive(true);
                targetPos = mapPlaceholders1[GetIndexValue(lvlNum, startNewMap) - 1].localPosition;
                // characterImage.transform.SetParent(mapPlaceholders1[GetIndexValue(lvlNum, startNewMap) - 1]);
            }
            else
            {
                if (lvlNum == 11)
                {
                    characterImage.gameObject.transform.localPosition = new Vector3(-800f, -950f, 0f);
                }

                characterImage.transform.SetParent(map2.transform);
                map2.SetActive(true);
                targetPos = mapPlaceholders2[GetIndexValue(lvlNum, startNewMap) - 1].localPosition;
                // characterImage.transform.SetParent(mapPlaceholders2[GetIndexValue(lvlNum, startNewMap) - 1]);
            }
        }
        else if (lvlNum < 31)
        {
            if (lvlNum == 21 && !isSceneSetup && !startNewMap)
            {
                isNewMap = true;
                map2.SetActive(true);
                targetPos = mapPlaceholders2[GetIndexValue(lvlNum, startNewMap) - 1].localPosition;
                // characterImage.transform.SetParent(mapPlaceholders2[GetIndexValue(lvlNum, startNewMap) - 1]);
            }
            else
            {
                if (lvlNum == 21)
                {
                    characterImage.gameObject.transform.localPosition = new Vector3(-800f, -950f, 0f);
                }

                characterImage.transform.SetParent(map3.transform);
                map3.SetActive(true);
                targetPos = mapPlaceholders3[GetIndexValue(lvlNum, startNewMap) - 1].localPosition;
                // characterImage.transform.SetParent(mapPlaceholders3[GetIndexValue(lvlNum, startNewMap) - 1]);
            }
        }
        else
        {
            if (lvlNum == 31 && !isSceneSetup && !startNewMap)
            {
                isNewMap = true;
                map3.SetActive(true);
                targetPos = mapPlaceholders3[GetIndexValue(lvlNum, startNewMap) - 1].localPosition;
                // characterImage.transform.SetParent(mapPlaceholders3[GetIndexValue(lvlNum, startNewMap) - 1]);
            }
            else
            {
                if (lvlNum == 31)
                {
                    characterImage.gameObject.transform.localPosition = new Vector3(-800f, -950f, 0f);
                }

                characterImage.transform.SetParent(map4.transform);
                map4.SetActive(true);
                targetPos = mapPlaceholders4[GetIndexValue(lvlNum, startNewMap) - 1].localPosition;
                // characterImage.transform.SetParent(mapPlaceholders4[GetIndexValue(lvlNum, startNewMap) - 1]);
            }
        }
        
        characterImage.GetComponent<Image>().sprite = Player.Instance.CurrentCharacter.defaultCharacter;
        characterImage.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.3f, 0.3f, 0.3f);

        if (isSceneSetup)
        {
            characterImage.gameObject.transform.localPosition = targetPos;
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
        AnimOverlayManager.Instance.StartWhiteScreenFadeLoadScreen();
        StartCoroutine(ProceedToNextLevel());
    }


    private IEnumerator ProceedToNextLevel()
    {
        yield return new WaitForSeconds(0.6f);
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
        characterImage.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.3f, 0.3f, 0.3f);
        yield return new WaitForSeconds(2.5f);
        float timeCounter = 0;
        float speed = 20f;

        while (timeCounter < speed)
        {
            if (Vector3.Distance(characterImage.gameObject.transform.localPosition, targetPos) < 2f)
            {
                break;
            }

            characterImage.gameObject.transform.localPosition = Vector3.Lerp(characterImage.gameObject.transform.localPosition, targetPos, timeCounter / speed);
            timeCounter += Time.deltaTime;
            yield return null;
        }

        characterImage.gameObject.transform.localPosition = targetPos;

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
