using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;


public class GameDataManager : MonoBehaviour
{
    [SerializeField] private GameStateHolder gameStateHolder;
    public static GameDataManager Instance {private set; get;}
    public Dictionary<string, int> playerRecords = new Dictionary<string, int>();
    private Dictionary<string, GameStateData> allPlayersGameStateData = new Dictionary<string, GameStateData>();
    public Dictionary<string, int> PlayerRecords { set{playerRecords = value;} get{return playerRecords;}}
    public Dictionary<string, GameStateData> AllPlayersGameStateData { set{allPlayersGameStateData = value;} get{return allPlayersGameStateData;}}


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
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
        string directoryPath = Application.persistentDataPath + "/DoNotDelete/";
        string filePath = directoryPath + "PlayerScores.json";

        try
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(filePath))
            {
                // If the file doesn't exist, create it
                string playerRecord = JsonConvert.SerializeObject(new Dictionary<string, int>());
                File.WriteAllText(filePath, Encrypt(playerRecord));
            }

            string jsonString = File.ReadAllText(filePath);

            // Deserialize into playerRecords using Json.NET
            playerRecords = JsonConvert.DeserializeObject<Dictionary<string, int>>(Decrypt(jsonString));
        }
        catch (Exception)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                if (!File.Exists(filePath))
                {
                    // If the file doesn't exist, create it
                    string playerRecord = JsonConvert.SerializeObject(new Dictionary<string, int>());
                    File.WriteAllText(filePath, Encrypt(playerRecord));
                }
            }
            catch (Exception)
            {
                return (false, "Delete/Clear Game Cache and Game Data before trying opening the game.");
            }
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
            File.WriteAllText(filePath, Encrypt(jsonString));
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


    private string Encrypt(string input)
    {
        // Simple XOR encryption with a key
        char xorKey = 'Q'; // Replace with your own key

        char[] encryptedChars = new char[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            encryptedChars[i] = (char)(input[i] ^ xorKey);
        }

        return new string(encryptedChars);
    }


    private string Decrypt(string input)
    {
        return Encrypt(input);
    }
    //===>> PLAYER RECORDS (NAME, SCORE) <<===//








    //===>> PLAYERS' GAME STATE DATA <<===//
    public ValueTuple<bool, string> SaveGameData(string playerName, GameStateData currentPlayerGameStateData) 
    {
        try
        {
            if (allPlayersGameStateData.ContainsKey(playerName))
            {
                allPlayersGameStateData[playerName] = currentPlayerGameStateData;
            }
            else
            {
                allPlayersGameStateData.Add(playerName, currentPlayerGameStateData);
            }

            string directoryPath = Application.persistentDataPath + "/DoNotDelete/";
            string filePath = directoryPath + "PlayerGameStateData.dat";

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Create(filePath);
            bf.Serialize(fileStream, allPlayersGameStateData);
            fileStream.Close();
        }
        catch (Exception e)
        {
            return (false, e.ToString());
        }

        return (true, "");
    }


    public ValueTuple<bool, string> SaveGameData() 
    {
        try
        {
            string directoryPath = Application.persistentDataPath + "/DoNotDelete/";
            string filePath = directoryPath + "PlayerGameStateData.dat";

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStreamSave = File.Create(filePath);
            bf.Serialize(fileStreamSave, allPlayersGameStateData);
            fileStreamSave.Close();
        }
        catch (Exception e)
        {
            return (false, e.ToString());
        }

        return (true, "");
    }


    public ValueTuple<bool, string> LoadGameData()
    {
        BinaryFormatter bf = new BinaryFormatter ();
        string directoryPath = Application.persistentDataPath + "/DoNotDelete/";
        string filePath = directoryPath + "PlayerGameStateData.dat";

        try
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(filePath))
            {
                FileStream fileStreamSave = File.Create(filePath);
                Dictionary<string, GameStateData> dataHolder = new Dictionary<string, GameStateData>();
                bf.Serialize(fileStreamSave, dataHolder);
                fileStreamSave.Close();
            }

            FileStream fileStreamLoad = File.Open (filePath, FileMode.Open);
            Dictionary<string, GameStateData> data = (Dictionary<string, GameStateData>)bf.Deserialize(fileStreamLoad);
            allPlayersGameStateData = data;
            fileStreamLoad.Close();
        }
        catch (Exception)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                if (!File.Exists(filePath))
                {
                    FileStream fileStreamSave = File.Create(filePath);
                    Dictionary<string, GameStateData> dataHolder = new Dictionary<string, GameStateData>();
                    bf.Serialize(fileStreamSave, dataHolder);
                    fileStreamSave.Close();
                }

                allPlayersGameStateData = new Dictionary<string, GameStateData>();
            }
            catch (Exception)
            {
                return (false, "Delete/Clear Game Cache and Game Data before trying opening the game.");
            }
        }

        return (true, "");
    }
}