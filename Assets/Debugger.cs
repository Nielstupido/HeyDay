using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Debugger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI errorText;
    public static Debugger Instance {private set; get;}


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


    public void ShowError(string error)
    {
        this.gameObject.SetActive(true);
        errorText.text = error;
    }
}
