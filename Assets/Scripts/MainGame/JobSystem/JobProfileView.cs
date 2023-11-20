using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JobProfileView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI playerPositionText;
    [SerializeField] TextMeshProUGUI playerEducationText;
    [SerializeField] TextMeshProUGUI playerCompanyText;
    [SerializeField] TextMeshProUGUI playerTotalWHtext;
    [SerializeField] TextMeshProUGUI playerCurrentWHLabel;
    [SerializeField] TextMeshProUGUI playerCurrentWHtext;
    [SerializeField] TextMeshProUGUI playerSalaryPerHr;
    private Player Player;


    public void SetupJobProfileView()
    {
        this.gameObject.SetActive(true);

        Player = Player.Instance;

        playerNameText.text = Player.PlayerName;

    }
}
