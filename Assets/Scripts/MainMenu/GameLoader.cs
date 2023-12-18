using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadScreen;
    [SerializeField] private Slider loadSlider;
    public static GameLoader Instance { get; private set; }


    private void Awake() 
    { 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void LoadGameScene()
    {
        StartCoroutine(LoadSceneAsync());
    }


    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainGame");
        int counter = 0;

        loadScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progressVal = Mathf.Clamp01(operation.progress / 0.9f);
            loadSlider.value = progressVal;
            counter++;
            yield return null;
        }
    }
}
