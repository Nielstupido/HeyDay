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
        playerPositionText.text = Player.CurrentPlayerJob.jobPosName;
        playerEducationText.text = GameManager.Instance.EnumStringParser(Player.PlayerEnrolledCourse);
        playerCompanyText.text = Player.CurrentPlayerJob.establishment.ToString();
        playerTotalWHtext.text = Player.GetTotalWorkHours().ToString() + "hrs";
        playerCurrentWHLabel.text = "Current Work Hours [" + GameManager.Instance.EnumStringParser(Player.CurrentPlayerJob.workField) +"]";
        playerCurrentWHtext.text = Player.CurrentWorkHours.ToString() + "hrs";
        playerCurrentWHtext.text = "₱" + Player.CurrentPlayerJob.salaryPerHr.ToString();
    }
}
