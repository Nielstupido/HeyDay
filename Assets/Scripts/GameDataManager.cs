using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;


public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance {private set; get;}
    public Dictionary<string, int> playerRecords = new Dictionary<string, int>();
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
            string directoryPath = Application.persistentDataPath + "/DoNotDelete/";
            string filePath = directoryPath + "PlayerScores.json";

            // Check if the directory exists, create it if not
            if (!Directory.Exists(directoryPath))
            {
                Debug.Log("TEST 1: Directory mo wala");
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(filePath))
            {
                // If the file doesn't exist, create it
                string playerRecord = JsonConvert.SerializeObject(new Dictionary<string, int>());
                File.WriteAllText(filePath, playerRecord);
            }

            string jsonString = File.ReadAllText(filePath);

            // Deserialize into playerRecords using Json.NET
            playerRecords = JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonString);

            // Debug.Log each entry
            // foreach (var entry in playerRecords)
            // {
            //     Debug.Log($"Player: {entry.Key}, Score: {entry.Value}");
            // }
        }
        catch (Exception e)
        {
            Debug.LogError($"{e}");
            return (false, e.ToString());
        }

        return (true, "");
    }

    public ValueTuple<bool, string> SavePlayerRecords(string playerName, int score)
    {
        try
        {
            if (playerRecords.ContainsKey(playerName))
            {
                playerRecords[playerName] += score;
            }
            else
            {
                playerRecords.Add(playerName, score);
            }
    
            string directoryPath = Application.persistentDataPath + "/DoNotDelete/";
            string filePath = directoryPath + "PlayerScores.json";

            // Serialize playerRecords using Json.NET
            string jsonString = JsonConvert.SerializeObject(playerRecords);

            // Write the serialized data to the file
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
