using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq;

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
    public bool IsPlayerNameAvailable(string name)
    {
        foreach (var item in playerRecords)
        {
            if (item.Key == name)
            {
                return false;
            }
        }

        return true;
    }


    public ValueTuple<bool, string> LoadPlayerRecords()
    {
        try
        {
            string filePath = Application.persistentDataPath + "/DoNotDelete/PlayerScores.json";
            if (!File.Exists(filePath))
            {
                string playerRecord = JsonUtility.ToJson(new Dictionary<string, int>());
                File.WriteAllText(filePath, playerRecord);
            }
            
            string jsonString = File.ReadAllText(filePath);
            playerRecords = JsonUtility.FromJson<Dictionary<string, int>>(jsonString);
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
            string filePath = Application.persistentDataPath + "/DoNotDelete/PlayerScores.json";
            string jsonString = JsonUtility.ToJson(playerRecords);
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

        foreach (var item in playerRecords.OrderByDescending(item => item.Value))
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
            string filePath = Application.persistentDataPath + "/DoNotDelete/PlayerGameStateData.json";
            if (!File.Exists(filePath))
            {
                string playerGameStateData = JsonUtility.ToJson(new GameStateData());
                File.WriteAllText(filePath, playerGameStateData);
            }

            string jsonString = File.ReadAllText(filePath);
            allPlayersGameStateData = JsonUtility.FromJson<List<GameStateData>>(jsonString);
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

            string filePath = Application.persistentDataPath + "/DoNotDelete/PlayerGameStateData.json";
            string jsonString = JsonUtility.ToJson(allPlayersGameStateData);
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
            return (null, e.ToString());
        }

        return (currentGameState, "");
    }


    public void NewGameState()
    {
        GameManager.Instance.CurrentGameStateData = new GameStateData();
        SaveGameStateData(new GameStateData());
    }
    //===>> PLAYERS' GAME STATE DATA <<===//
}
