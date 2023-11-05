using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour 
{
    public Sprite[] sprites;
    public float StopTime;

    private bool isRandomizing = false;

    void Update () {
        if(isRandomizing)
        {
            RandomingImage();
        }
    }

    void RandomingImage(){
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = sprites [Random.Range (0, sprites.Length)];
    }

    IEnumerator RandomizeCoroutine()
    {
        isRandomizing = true;

        float startTime = Time.time;
        float endTime = startTime + StopTime;

        while (Time.time < endTime)
        {
            yield return null;
        }

        isRandomizing = false;

        // Store the last sprite in results list
        SlotMachineResults.Instance.results.Add(gameObject.GetComponent<UnityEngine.UI.Image>().sprite);
        SlotMachineResults.Instance.CheckForMatches();
    }

    public void StartRand(){
        StartCoroutine(RandomizeCoroutine());
        SlotMachineResults.Instance.results.Clear();
    }
}