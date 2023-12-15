using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField] private GameObject characterCreationOverlay;
    [SerializeField] private GameObject selectGenderOverlay;
    [SerializeField] private GameObject boyButton;
    [SerializeField] private GameObject girlButton;
    [SerializeField] private GameObject selectCharacterOverlay;
    [SerializeField] private Transform charactersHolder;
    [SerializeField] private GameObject charObj1;
    [SerializeField] private GameObject charObj2;
    [SerializeField] private GameObject charObj3;
    [SerializeField] private GameObject setNameOverlay;
    [SerializeField] private GameObject setNamePopUp;
    [SerializeField] private TextMeshProUGUI playerNameTextInput;
    [SerializeField] private TextMeshProUGUI playerNameTextDisplay;
    [SerializeField] private IntroCutsceneMannager introCutsceneMannager;
    [SerializeField] private Image playerBustIcon;
    [SerializeField] private Prompts usernameUnavailable;
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
        string playerName = playerNameTextInput.text.TrimEnd();
        Player.Instance.PlayerName = playerName;
        playerNameTextDisplay.text = playerName;

        if (!GameDataManager.Instance.IsPlayerNameAvailable(playerName))
        {
            PromptManager.Instance.ShowPrompt(usernameUnavailable);
            return;
        }

        GameDataManager.Instance.SavePlayerRecords(playerName, 0);
        StartCoroutine(ProceedIntro());
    }


    public void OpenCharacterCreationOVerlay()
    {
        characterCreationOverlay.SetActive(true);
        selectGenderOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimShowObj(boyButton);
        OverlayAnimations.Instance.AnimShowObj(girlButton);
        selectCharacterOverlay.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(setNamePopUp, setNameOverlay);
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
        
        OverlayAnimations.Instance.AnimHideObj(boyButton, selectGenderOverlay);
        OverlayAnimations.Instance.AnimHideObj(girlButton, selectGenderOverlay);
        selectGenderOverlay.SetActive(false);
        ShowCharacters();
    }

    public void SelectCharBack()
    {
        OverlayAnimations.Instance.AnimHideObj(charObj1, selectCharacterOverlay);
        OverlayAnimations.Instance.AnimHideObj(charObj2, selectCharacterOverlay);
        OverlayAnimations.Instance.AnimHideObj(charObj3, selectCharacterOverlay);
    }


    private void ShowCharacters()
    {
        selectCharacterOverlay.SetActive(true);
        OverlayAnimations.Instance.AnimShowObj(charObj1);
        OverlayAnimations.Instance.AnimShowObj(charObj2);
        OverlayAnimations.Instance.AnimShowObj(charObj3);

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
        OverlayAnimations.Instance.AnimOpenOverlay(setNamePopUp);
        playerBustIcon.sprite = currentCharacters.Find((characterItem) => characterItem.characterID == characterID).bustIcon;
        playerBustIcon.SetNativeSize();
    }
}
