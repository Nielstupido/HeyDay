using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CinemaManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI refNumber;
    [SerializeField] private Button watchMovieBtn;
    [SerializeField] private GameObject cinemaOverlay;
    [SerializeField] private Prompts notEnoughMoney;
    private float ticketPrice = 250f;


    private void Start()
    {
        watchMovieBtn.onClick.AddListener(WatchMovie);
    }


    private void WatchMovie()
    {
        if (Player.Instance.Pay(ticketPrice, 2f, 15f, notEnoughMoney))
        {
            StartCoroutine(WatchingMovie(2f));
        }
    }


    private IEnumerator WatchingMovie(float waitingTime)
    {
        AnimOverlayManager.Instance.StartAnim(ActionAnimations.WATCHMOVIE);
        yield return new WaitForSeconds(waitingTime);
        cinemaOverlay.SetActive(false);
        AnimOverlayManager.Instance.StopAnim();
        yield return null;
    }
}
