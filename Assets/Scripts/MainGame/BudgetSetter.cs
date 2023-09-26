using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BudgetSetter : MonoBehaviour
{
    [SerializeField] private Slider billsSlider;
    [SerializeField] private TextMeshProUGUI billsSliderAmountText;
    [SerializeField] private Slider savingsSlider;
    [SerializeField] private TextMeshProUGUI savingsSliderAmountText;
    [SerializeField] private Slider consumablesSlider;
    [SerializeField] private TextMeshProUGUI consumablesSliderAmountText;
    [SerializeField] private Slider emergencySlider;
    [SerializeField] private TextMeshProUGUI emergencySliderAmountText;


    private void Start()
    {
        billsSlider.onValueChanged.AddListener((v) => {
            billsSliderAmountText.text = v.ToString("0");
        });
        savingsSlider.onValueChanged.AddListener((v) => {
            savingsSliderAmountText.text = v.ToString("0");
        });
        consumablesSlider.onValueChanged.AddListener((v) => {
            consumablesSliderAmountText.text = v.ToString("0");
        });
        emergencySlider.onValueChanged.AddListener((v) => {
            emergencySliderAmountText.text = v.ToString("0");
        });
    }


    public void SaveBudget()
    {
        Debug.Log(int.Parse(billsSliderAmountText.text));
        Debug.Log(int.Parse(savingsSliderAmountText.text));
        Debug.Log(int.Parse(consumablesSliderAmountText.text));
        Debug.Log(int.Parse(emergencySliderAmountText.text));
    }
}

