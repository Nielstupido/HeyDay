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
    public RelStatus relStatus;
    public int relStatBarValue;
    public float currentDebt;
    public bool numberObtained = false;
}
