using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersObj : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    private PlayerInfoManager playerInfoManager;
    public int characterID;


    public void SetupCharacter(CharactersScriptableObj characterData, PlayerInfoManager currentPlayerInfoManager)
    {
        this.characterID = characterData.characterID;
        characterImage.sprite = characterData.defaultCharacter;
        playerInfoManager = currentPlayerInfoManager;
        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate{playerInfoManager.OnCharacterSelected(this.characterID);});
    }
}
