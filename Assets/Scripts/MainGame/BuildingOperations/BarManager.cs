using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI refNumber;
    [SerializeField] private Button partyBtn;
    [SerializeField] private GameObject barOverlay;
    [SerializeField] private Prompts notEnoughMoney;
    private float barEntrance = 500f;


    private void Start()
    {
        partyBtn.onClick.AddListener(Party);
        refNumber.text = Random.Range(11000, 100000).ToString();
    }


    private void Party()
    {
        if (Player.Instance.Pay(false, barEntrance, 2f, 80f, 15f, notEnoughMoney, 15f))
        {
            LevelManager.onFinishedPlayerAction(MissionType.PARTY);
            StartCoroutine(StartParty(2f));
        }
    }


    private IEnumerator StartParty(float waitingTime)
    {
        AnimOverlayManager.Instance.StartAnim(ActionAnimations.PARTY);
        yield return new WaitForSeconds(waitingTime);
        barOverlay.SetActive(false);
        AnimOverlayManager.Instance.StopAnim();
        yield return null;
    }
}
