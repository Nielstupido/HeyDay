using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private GameObject selectGenderOverlay;
    [SerializeField] private GameObject setNameOverlay;
    [SerializeField] private IntroCutsceneMannager introCutsceneMannager;


    public void StartIntroScene()
    {
        Player.Instance.PlayerName = playerNameText.text;
        introCutsceneMannager.StartIntro();
        this.gameObject.SetActive(false);
    }


    public void SetGender(bool isBoy)
    {
        if (isBoy)
        {
            Player.Instance.PlayerGender = Gender.MALE;
            Debug.Log("it's a boy!");
        }
        else
        {
            Player.Instance.PlayerGender = Gender.FEMALE;
            //gender selected girl
            Debug.Log("it's a girl!");
        }
        selectGenderOverlay.SetActive(false);
        setNameOverlay.SetActive(true);
    }


    public void SetAge(int ageValue)
    {

    }
}
