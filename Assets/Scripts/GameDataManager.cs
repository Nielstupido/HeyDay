using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance {private set; get;}
    private Dictionary<string, int> playerRecords = new Dictionary<string, int>();
    private List<GameStateData> allPlayersGameStateData;
    public Dictionary<string, int> PlayerRecords { set{playerRecords = value;} get{return playerRecords;}}


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

        DontDestroyOnLoad(this.gameObject);
    }


    //===>> PLAYER RECORDS (NAME, SCORE) <<===//
    public ValueTuple<bool, string> LoadPlayerRecords()
    {
        try
        {
            string filePath = Application.dataPath + "/DoNotDelete/PlayerScores.json";
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "{}");
            }
            
            string jsonString = File.ReadAllText(filePath);
            playerRecords = JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonString);
        }
        catch (Exception e)
        {
            return (false, e.ToString());
        }

        return (true, "");
    }


    public ValueTuple<bool, string> SavePlayerRecords(string playerName, int score)
    {
        try
        {
            if (!playerRecords.ContainsKey(playerName))
            {
                playerRecords.Add(playerName, 0);
            }
            playerRecords[playerName] += score;
            string filePath = Application.dataPath + "/DoNotDelete/PlayerScores.json";
            string jsonString = JsonConvert.SerializeObject(playerRecords);
            File.WriteAllText(filePath, jsonString);
        }
        catch (Exception e)
        {
            return (false, e.ToString());
        }

        return (true, "");
    }


    public Dictionary<string, int> GetCurrentLevelScores()
    {
        Dictionary<string, int> currentLevelScores = new Dictionary<string, int>();

        foreach (var item in playerRecords)
        {
            try
            {
                currentLevelScores.Add(item.Key, item.Value);
            }
            catch (Exception)
            {
                continue;
            }
        }

        return currentLevelScores;
    }
    //===>> PLAYER RECORDS (NAME, SCORE) <<===//



    //===>> PLAYERS' GAME STATE DATA <<===//
    public ValueTuple<bool, string> LoadAllGameStateData()
    {
        try
        {
            string filePath = Application.dataPath + "/DoNotDelete/PlayerGameStateData.json";
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "{}");
            }
            
            string jsonString = File.ReadAllText(filePath);
            allPlayersGameStateData = JsonConvert.DeserializeObject<List<GameStateData>>(jsonString);
        }
        catch (Exception e)
        {
            return (false, e.ToString());
        }

        return (true, "");
    }


    public ValueTuple<bool, string> SaveGameStateData(GameStateData currentPlayerGameStateData)
    {
        try
        {
            if (allPlayersGameStateData.Contains(currentPlayerGameStateData))
            {
                allPlayersGameStateData[allPlayersGameStateData.IndexOf(currentPlayerGameStateData)] = currentPlayerGameStateData;
            }
            else
            {
                allPlayersGameStateData.Add(currentPlayerGameStateData);
            }

            string filePath = Application.dataPath + "/DoNotDelete/PlayerGameStateData.json";
            string jsonString = JsonConvert.SerializeObject(allPlayersGameStateData);
            File.WriteAllText(filePath, jsonString);
        }
        catch (Exception e)
        {
            return (false, e.ToString());
        }

        return (true, "");
    }

    
    public ValueTuple<GameStateData, string> GetCurrentGameState(string playerName)
    {
        GameStateData currentGameState = null;
        try
        {
            currentGameState = allPlayersGameStateData.Find( (gameState) => gameState.playerName == playerName);
        }
        catch (Exception e)
        {
            return (currentGameState, e.ToString());
        }

        return (currentGameState, "");
    }
    //===>> PLAYERS' GAME STATE DATA <<===//
}
