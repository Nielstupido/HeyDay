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
        AudioManager.Instance.PlaySFX("Select");
        if (Player.Instance.Pay(false, barEntrance, 2f, 80f, 15f, notEnoughMoney, 15f))
        {
            LevelManager.onFinishedPlayerAction(MissionType.PARTY);
            StartCoroutine(StartParty(5f));
        }
    }


    private IEnumerator StartParty(float waitingTime)
    {
        AnimOverlayManager.Instance.StartAnim(ActionAnimations.PARTY);
        AudioManager.Instance.PlaySFX("Party");
        yield return new WaitForSeconds(waitingTime);
        barOverlay.SetActive(false);
        AnimOverlayManager.Instance.StopAnim();
        yield return null;
    }
}
