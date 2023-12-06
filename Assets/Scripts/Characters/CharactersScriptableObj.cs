using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu( menuName = "Character Asset Container")]
public class CharactersScriptableObj : ScriptableObject
{
    public string characterName;
    public Gender characterGender;
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
    public bool beenFriends;
    public Buildings currentBuildiing;
    private int socialEnergyLvl = 0;
    private int currentSocialEnergyLvl = 0;
    private int randomNum;
    private int percentage;

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
                            "Wazzuuup"
                            };
    private string[] greetingsEnemies = {
                            "??",
                            "What do you need???",
                            "What?!"
                            };

    //Bye-byes
    private string[] byeStrangers = {
                            "Eh",
                            "Find someone else"
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
                            "Don't get run over",
                            "See you later, alligator"
                            };
    private string[] byeEnemies = {
                            "Bye",
                            "Nah bye"
                            };

    //Rejections
    private string[] rejectStrangers = {
                            "Weirdo!",
                            "Get away from me!"
                            };
    private string[] rejectFriends = {
                            "No thanks hehe",
                            "Hmm not now"
                            };
    private string[] rejectGoodFriends = {
                            "No thanks hehe",
                            "Hmm not now"
                            };
    private string[] rejectBestBuddies = {
                            "No thanks hehe",
                            "Hmm not now"
                            };
    private string[] rejectEnemies = {
                            "No thanks hehe",
                            "Hmm not now"
                            };


    //<<<<<<<<<<<<<<<<<<<< PUBLIC METHODS >>>>>>>>>>>>>>>>>>>>>>//
    public void PrepareCharacter(string name, int relStatVal)
    {
        socialEnergyLvl = UnityEngine.Random.Range(1, 3);
        currentSocialEnergyLvl = socialEnergyLvl;
        characterName = name;
        relStatBarValue = relStatVal;
        beenFriends = false;
    }
    

    public string Interact()
    {
        currentSocialEnergyLvl = socialEnergyLvl;

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
        currentSocialEnergyLvl = socialEnergyLvl;
    }


    public string SayBye()
    {
        switch (this.relStatus)
        {
            case RelStatus.STRANGERS:
                randomNum = UnityEngine.Random.Range(0, byeStrangers.Length);
                return byeStrangers[randomNum];
            case RelStatus.FRIENDS:
                randomNum = UnityEngine.Random.Range(0, byeFriends.Length);
                return byeFriends[randomNum];
            case RelStatus.GOOD_FRIENDS:
                randomNum = UnityEngine.Random.Range(0, byeGoodFriends.Length);
                return byeGoodFriends[randomNum];
            case RelStatus.BEST_BUDDIES:
                randomNum = UnityEngine.Random.Range(0, byeBestBuddies.Length);
                return byeBestBuddies[randomNum];
            case RelStatus.ENEMIES:
                randomNum = UnityEngine.Random.Range(0, byeEnemies.Length);
                return byeEnemies[randomNum];
            default:
                return "";
        }
    }


    public ValueTuple<bool, int, string> Chat()
    {
        if (currentSocialEnergyLvl == 0)
        {
            relStatBarValue--;
            CheckRelStatus();
            return (false, 0, SayBye());
        }
        
        relStatBarValue += 2;
        currentSocialEnergyLvl--;
        CheckRelStatus();
        return (true, currentSocialEnergyLvl, "");
    }


    public ValueTuple<bool, int, string> Hug()
    {
        switch (this.relStatus)
        {
            case RelStatus.STRANGERS:
                return NpcDecision(2, rejectStrangers);
            case RelStatus.FRIENDS:
                return NpcDecision(6, rejectFriends);
            case RelStatus.GOOD_FRIENDS:
                return NpcDecision(8, rejectGoodFriends);
            case RelStatus.BEST_BUDDIES:
                return NpcDecision(9, rejectBestBuddies);
            case RelStatus.ENEMIES:
                return NpcDecision(1, rejectEnemies);
            default:
                return (false, 0, "");
        }
    }


    public bool PayDebt()
    {
        relStatBarValue += 5;
        currentDebt = 0;
        return CheckRelStatus();;
    }


    public ValueTuple<bool, int, string> GetNumber()
    {
        currentSocialEnergyLvl--;
        CheckRelStatus();

        if (this.relStatus == RelStatus.STRANGERS || this.relStatus == RelStatus.ENEMIES)
        {
            randomNum = UnityEngine.Random.Range(1, 11);

            if (randomNum > (relStatBarValue / 4))
            {
                if (this.relStatus == RelStatus.STRANGERS)
                {
                    return (false, currentSocialEnergyLvl, GetRejected(rejectStrangers));
                }
                
                return (false, currentSocialEnergyLvl, GetRejected(rejectEnemies));
            }
        }
        
        numberObtained = true;
        relStatBarValue += 10;
        return (CheckRelStatus(), currentSocialEnergyLvl, "");
    }


    public ValueTuple<CharacterEmotions, CharacterStance, int> YellAt()
    {
        randomNum = UnityEngine.Random.Range(1, 11);

        if (relStatus == RelStatus.STRANGERS)
        {
            if (randomNum < 7)
            {
                relStatBarValue -= 5;
                currentSocialEnergyLvl -= 2;
                CheckRelStatus();
                return (CharacterEmotions.ANGRY, CharacterStance.DEFAULT, currentSocialEnergyLvl);
            }
        }
        else if (relStatus == RelStatus.ENEMIES)
        {
            relStatBarValue -= 5;
            currentSocialEnergyLvl -= 2;
            CheckRelStatus();
            return (CharacterEmotions.ANGRY, CharacterStance.DEFAULT, currentSocialEnergyLvl);
        }
        else
        {
            if (randomNum > ((relStatBarValue / 9) + (100 / relStatBarValue)))
            {
                relStatBarValue -= 10;
                currentSocialEnergyLvl -= 2;
                CheckRelStatus();
                return (CharacterEmotions.ANGRY, CharacterStance.DEFAULT, currentSocialEnergyLvl);
            }
        }

        return (CharacterEmotions.CONFFUSED, CharacterStance.CONFUSED, currentSocialEnergyLvl);
    } 


    public ValueTuple<CharacterEmotions, CharacterStance, int> TellJoke()
    {
        randomNum = UnityEngine.Random.Range(1, 11);

        if (relStatus == RelStatus.STRANGERS)
        {
            if (randomNum > 6)
            {
                relStatBarValue += 8;
                currentSocialEnergyLvl --;
                CheckRelStatus();
                return (CharacterEmotions.HAPPY, CharacterStance.DEFAULT, currentSocialEnergyLvl);
            }
        }
        else if (relStatus == RelStatus.ENEMIES)
        {
            if (randomNum > 5)
            {
                relStatBarValue += 8;
                currentSocialEnergyLvl --;
                CheckRelStatus();
                return (CharacterEmotions.HAPPY, CharacterStance.DEFAULT, currentSocialEnergyLvl);
            }
        }
        else
        {
            if (randomNum > ((relStatBarValue / 9) + (100 / relStatBarValue)))
            {
                relStatBarValue += 5;
                currentSocialEnergyLvl --;
                CheckRelStatus();
                return (CharacterEmotions.HAPPY, CharacterStance.DEFAULT, currentSocialEnergyLvl);
            }
        }

        return (CharacterEmotions.CONFFUSED, CharacterStance.CONFUSED, currentSocialEnergyLvl);
    } 


    public ValueTuple<bool, int, string> TryBorrowMoney()
    {
        switch (this.relStatus)
        {
            case RelStatus.STRANGERS:
                return NpcDecision(3, rejectStrangers);
            case RelStatus.FRIENDS:
                return NpcDecision(6, rejectFriends);
            case RelStatus.GOOD_FRIENDS:
                return NpcDecision(8, rejectGoodFriends);
            case RelStatus.BEST_BUDDIES:
                return NpcDecision(9, rejectBestBuddies);
            case RelStatus.ENEMIES:
                return NpcDecision(2, rejectEnemies);
            default:
                return (false, 0, "");
        }
    }
    //<<<<<<<<<<<<<<<<<<<< PUBLIC METHODS >>>>>>>>>>>>>>>>>>>>>>//




    //<<<<<<<<<<<<<<<<<<<< PRIVATE METHODS >>>>>>>>>>>>>>>>>>>>>>//
    private string GetRejected(string[] rejections)
    {
        relStatBarValue--;
        CheckRelStatus();
        randomNum = UnityEngine.Random.Range(0, rejections.Length);
        return rejections[randomNum];
    }


    private ValueTuple<bool, int, string> NpcDecision(int possibilityRate, string[] rejections)
    {
        if (currentSocialEnergyLvl == 0)
        {
            relStatBarValue--;
            CheckRelStatus();
            randomNum = UnityEngine.Random.Range(0, rejections.Length);
            return (false, currentSocialEnergyLvl, rejections[randomNum]);
        }

        randomNum = UnityEngine.Random.Range(1, 11);

        if (randomNum <= possibilityRate)
        {
            relStatBarValue += 5;
            CheckRelStatus();
            return (true, currentSocialEnergyLvl, "");
        }

        currentSocialEnergyLvl--;
        randomNum = UnityEngine.Random.Range(0, rejections.Length);
        CheckRelStatus();
        return (false, currentSocialEnergyLvl, rejections[randomNum]);
    }


    private bool CheckRelStatus()
    {
        if (currentSocialEnergyLvl < 0)
        {
            currentSocialEnergyLvl = 0;
        }

        if (relStatBarValue < 0)
        {
            relStatBarValue = 0;
        }

        if (relStatBarValue > 100)
        {
            relStatBarValue = 100;
        }

        if (beenFriends)
        {
            switch (relStatBarValue)
            {
                case <= 30:
                    relStatus = RelStatus.ENEMIES;
                    break;
                case <= 60:
                    relStatus = RelStatus.FRIENDS;
                    break;
                case <= 80:
                    relStatus = RelStatus.GOOD_FRIENDS;
                    break;
                case <= 100:
                    relStatus = RelStatus.BEST_BUDDIES;
                    break;
            }
        }
        else
        {
            switch (relStatBarValue)
            {
                case <= 30:
                    relStatus = RelStatus.ENEMIES;
                    break;
                case <= 60:
                    relStatus = RelStatus.FRIENDS;
                    beenFriends = true;
                    break;
            }
        }
        return true;
    }
    //<<<<<<<<<<<<<<<<<<<< PRIVATE METHODS >>>>>>>>>>>>>>>>>>>>>>//
}
