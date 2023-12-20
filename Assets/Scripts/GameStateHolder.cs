using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data Holder", fileName = "Game Data")]
public class GameStateHolder : ScriptableObject
{
    public Dictionary<string, GameStateData> allPlayerData;
}
