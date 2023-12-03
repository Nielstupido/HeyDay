using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu( menuName = "Character Asset Container")]
public class CharactersScriptableObj : ScriptableObject
{
    public int characterID;
    public Sprite bustIcon;
    public Sprite defaultCharacter;
    public Sprite defaultBody;
    public Sprite confusedBody;
    public Sprite armRaisedBody;
    public Sprite defaultEmo;
    public Sprite angryEmo;
    public Sprite happyEmo;
    public Sprite sadEmo;
    public Sprite confusedEmo;
    public Sprite neutralEmo;
    public RelStatus relStatus = RelStatus.STRANGERS;
    public int relStatBarValue = 0;
    public float currentDebt = 0f;
    public bool numberObtained = false;
    private int patienceLevel = 0;
    private int currentPatienceLevel = 0;
    private int randomNum;

    //Greetings
    private string[] greetingsStrangers = {
                            "Yes?",
                            "Whut?",
                            "Hm?"
                            };
    private string[] greetingsFriends = {
                            "Hi",
                            "Hello"
                            };
    private string[] greetingsGoodFriends = {
                            "Hi!",
                            "Hello!",
                            "Zup!"
                            };
    private string[] greetingsBestBuddies = {
                            "Hey!!",
                            "Wazzuuup",
                            };
    private string[] greetingsEnemies = {
                            "??",
                            "What do you need???",
                            "What?!"
                            };

    //Bye-byes
    private string[] byeStrangers = {
                            "Eh",
                            "Find someone else",
                            };
    private string[] byeFriends = {
                            "Bye sorry",
                            "Sorry gtg"
                            };
    private string[] byeGoodFriends = {
                            "I got to go, I'm sorry",
                            "My apologies, I need to go"
                            };
    private string[] byeBestBuddies = {
                            "Hey!!",
                            "Wazzuuup",
                            };
    private string[] byeEnemies = {
                            "Bye",
                            "Nah bye"
                            };


    public void PrepareCharacter()
    {
        patienceLevel = UnityEngine.Random.Range(1, 3);
        currentPatienceLevel = patienceLevel;
    }
    

    public string Interact()
    {
        switch (this.relStatus)
        {
            case RelStatus.STRANGERS:
                randomNum = UnityEngine.Random.Range(0, greetingsStrangers.Length);
                return greetingsStrangers[randomNum];
            case RelStatus.FRIENDS:
                randomNum = UnityEngine.Random.Range(0, greetingsFriends.Length);
                return greetingsFriends[randomNum];
            case RelStatus.GOOD_FRIENDS:
                randomNum = UnityEngine.Random.Range(0, greetingsGoodFriends.Length);
                return greetingsGoodFriends[randomNum];
            case RelStatus.BEST_BUDDIES:
                randomNum = UnityEngine.Random.Range(0, greetingsBestBuddies.Length);
                return greetingsBestBuddies[randomNum];
            case RelStatus.ENEMIES:
                randomNum = UnityEngine.Random.Range(0, greetingsEnemies.Length);
                return greetingsEnemies[randomNum];
            default:
                return null;
        }
    }


    public void OnInteractionEnded()
    {
        currentPatienceLevel = patienceLevel;
    }


    public ValueTuple<bool, int, string> Chat()
    {
        relStatBarValue += 2;
        currentPatienceLevel--;

        if (currentPatienceLevel == 0)
        {
            relStatBarValue--;

            switch (this.relStatus)
            {
                case RelStatus.STRANGERS:
                    randomNum = UnityEngine.Random.Range(0, byeStrangers.Length);
                    return (false, 0, byeStrangers[randomNum]);
                case RelStatus.FRIENDS:
                    randomNum = UnityEngine.Random.Range(0, byeFriends.Length);
                    return (false, 0, byeFriends[randomNum]);
                case RelStatus.GOOD_FRIENDS:
                    randomNum = UnityEngine.Random.Range(0, byeGoodFriends.Length);
                    return (false, 0, byeGoodFriends[randomNum]);
                case RelStatus.BEST_BUDDIES:
                    randomNum = UnityEngine.Random.Range(0, byeBestBuddies.Length);
                    return (false, 0, byeBestBuddies[randomNum]);
                case RelStatus.ENEMIES:
                    randomNum = UnityEngine.Random.Range(0, byeEnemies.Length);
                    return (false, 0, byeEnemies[randomNum]);
                default:
                    return (false, 0, "");
            }
        }
    
        return (true, currentPatienceLevel, "");
    }
}
