using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SavedGameObj : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public Button button;


    public void SetupSavedGames(string name)
    {
        this.playerName.text = name;
        this.button.onClick.AddListener( () => {LoadSavedGame(name);});
    }


    public void LoadSavedGame(string name)
    {
        Debug.Log("load saved game of " + name);
    }
}
