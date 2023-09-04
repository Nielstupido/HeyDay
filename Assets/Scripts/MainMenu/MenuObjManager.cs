using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObjManager : MonoBehaviour
{
    public delegate void OnObjDestroyed(string parent);
    public static OnObjDestroyed onObjDestroyed;

    public delegate void OnGameStart();
    public static OnGameStart onGameStart;

    public delegate void OnGameDictionary();
    public static OnGameDictionary onGameDictionary;

    public delegate void OnBtnSet();
    public static OnBtnSet onBtnSet;

    public delegate void OnExitGameDictionary();
    public static OnExitGameDictionary onExitGameDictionary;
}
