using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SavedGameObj : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public Button loadBtn;
    public Button deleteBtn;


    public void SetupSavedGames(string name)
    {
        this.playerName.text = name;
        this.loadBtn.onClick.AddListener( () => {LoadSavedGame(name);});
        this.deleteBtn.onClick.AddListener( () => {DeleteSavedGame(name);});
    }


    public void LoadSavedGame(string name)
    {
        GameModeManager.Instance.LoadGame(name);
    }


    public void DeleteSavedGame(string name)
    {
        GameModeManager.Instance.DeleteGameSave(name);
    }
}
