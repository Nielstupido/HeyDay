using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObjManager : MonoBehaviour
{
    public delegate void OnObjDestroyed(string parent);
    public static OnObjDestroyed onObjDestroyed;

    public delegate void OnGameStart();
    public static OnGameStart onGameStart;

    public delegate void OnBtnSet();
    public static OnBtnSet onBtnSet;
}
