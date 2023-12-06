using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersObj : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    private PlayerInfoManager playerInfoManager;
    public int characterID;


    public void SetupCharacter(CharactersScriptableObj characterData, bool onCreationCharacOverlay, PlayerInfoManager currentPlayerInfoManager = null)
    {
        this.characterID = characterData.characterID;
        characterImage.sprite = characterData.defaultCharacter;
        
        if (onCreationCharacOverlay)
        {
            playerInfoManager = currentPlayerInfoManager;
            this.gameObject.GetComponent<Button>().onClick.AddListener(delegate{playerInfoManager.OnCharacterSelected(this.characterID);});
        }
        else
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(delegate{GameManager.Instance.InteractWithNPC(characterData.characterName);;});
        }
    }
}
