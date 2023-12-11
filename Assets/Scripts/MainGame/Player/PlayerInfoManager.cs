using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField] private GameObject characterCreationOverlay;
    [SerializeField] private GameObject selectGenderOverlay;
    [SerializeField] private GameObject selectCharacterOverlay;
    [SerializeField] private Transform charactersHolder;
    [SerializeField] private GameObject setNameOverlay;
    [SerializeField] private TextMeshProUGUI playerNameTextInput;
    [SerializeField] private TextMeshProUGUI playerNameTextDisplay;
    [SerializeField] private IntroCutsceneMannager introCutsceneMannager;
    [SerializeField] private Image playerBustIcon;
    private List<CharactersScriptableObj> currentCharacters = new List<CharactersScriptableObj>();


    private IEnumerator ProceedIntro()
    {
        AnimOverlayManager.Instance.StartBlackScreenFadeLoadScreen();
        yield return new WaitForSeconds(0.6f);
        characterCreationOverlay.SetActive(false);
        introCutsceneMannager.StartIntro();
    }


    public void StartIntroScene()
    {
        Player.Instance.PlayerName = playerNameTextInput.text;
        playerNameTextDisplay.text = playerNameTextInput.text;
        StartCoroutine(ProceedIntro());
    }


    public void OpenCharacterCreationOVerlay()
    {
        characterCreationOverlay.SetActive(true);
        selectGenderOverlay.SetActive(true);
        selectCharacterOverlay.SetActive(false);
        setNameOverlay.SetActive(false);
    }


    public void SetGender(bool isBoy)
    {
        if (isBoy)
        {
            Player.Instance.PlayerGender = Gender.MALE;
        }
        else
        {
            Player.Instance.PlayerGender = Gender.FEMALE;
        }
        selectGenderOverlay.SetActive(false);
        selectCharacterOverlay.SetActive(true);
        ShowCharacters();
    }


    private void ShowCharacters()
    {
        if (Player.Instance.PlayerGender == Gender.MALE)
        {
            currentCharacters = GameManager.Instance.Characters.GetRange(0, 3);
        }
        else
        {
            currentCharacters = GameManager.Instance.Characters.GetRange(3, 3);
        }

        for (int i = 0; i < currentCharacters.Count; i++)
        {
            charactersHolder.GetChild(i).GetComponent<CharactersObj>().SetupCharacter(currentCharacters[i], true, this);
        }
    }


    public void OnCharacterSelected(int characterID)
    {
        Player.Instance.CurrentCharacter = currentCharacters.Find((characterItem) => characterItem.characterID == characterID);
        GameManager.Instance.Characters.Remove(Player.Instance.CurrentCharacter);
        selectCharacterOverlay.SetActive(false);
        setNameOverlay.SetActive(true);
        playerBustIcon.sprite = currentCharacters.Find((characterItem) => characterItem.characterID == characterID).bustIcon;
        playerBustIcon.SetNativeSize();
    }
}
