using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BudgetSetter : MonoBehaviour
{
    //Slider
    [SerializeField] private Slider billsSlider;
    [SerializeField] private Slider billsSliderRecom;
    [SerializeField] private TextMeshProUGUI billsSliderAmountText;
    [SerializeField] private Slider savingsSlider;
    [SerializeField] private Slider savingsSliderRecom;
    [SerializeField] private TextMeshProUGUI savingsSliderAmountText;
    [SerializeField] private Slider consumablesSlider;
    [SerializeField] private Slider consumablesSliderRecom;
    [SerializeField] private TextMeshProUGUI consumablesSliderAmountText;
    [SerializeField] private Slider emergencySlider;
    [SerializeField] private Slider emergencySliderRecom;
    [SerializeField] private TextMeshProUGUI emergencySliderAmountText;

    [SerializeField] private TextMeshProUGUI playerCurrentMoneyText;
    private float billsOldVal;
    private float savingsOldVal;
    private float consumablesOldVal;
    private float emergencyOldVal;
    private float moneyValue;


    public void PrepareBudgeSetter(float currentPlayerMoney)
    {
        this.gameObject.SetActive(true);

        playerCurrentMoneyText.text = currentPlayerMoney.ToString();
        moneyValue = currentPlayerMoney;
        
        billsOldVal = 0f;
        savingsOldVal = 0f;
        consumablesOldVal = 0f;
        emergencyOldVal = 0f;

        billsSlider.maxValue = currentPlayerMoney;
        billsSliderRecom.maxValue = currentPlayerMoney;
        savingsSlider.maxValue = currentPlayerMoney;
        savingsSliderRecom.maxValue = currentPlayerMoney;
        consumablesSlider.maxValue = currentPlayerMoney;
        consumablesSliderRecom.maxValue = currentPlayerMoney;
        emergencySlider.maxValue = currentPlayerMoney;
        emergencySliderRecom.maxValue = currentPlayerMoney;

        billsSlider.value = billsOldVal;
        savingsSlider.value = savingsOldVal;
        consumablesSlider.value = consumablesOldVal;
        emergencySlider.value = emergencyOldVal;

        billsSliderAmountText.text = billsSlider.value.ToString("0");
        savingsSliderAmountText.text = savingsSlider.value.ToString("0");
        consumablesSliderAmountText.text = consumablesSlider.value.ToString("0");
        emergencySliderAmountText.text = emergencySlider.value.ToString("0");

        billsSlider.onValueChanged.AddListener((v) => {
            if (v > GetAmountLeft(1))
            {
                billsSlider.value = billsOldVal;
            }
            else
            {
                billsSlider.value = v;
            }
            float newVal = billsSlider.value;
            billsSliderAmountText.text = newVal.ToString("0");
            billsOldVal = newVal;
        });

        savingsSlider.onValueChanged.AddListener((v) => {
            if (v > GetAmountLeft(2))
            {
                savingsSlider.value = savingsOldVal;
            }
            else
            {
                savingsSlider.value = v;
            }

            float newVal = savingsSlider.value;
            savingsSliderAmountText.text = newVal.ToString("0");
            savingsOldVal = savingsSlider.value;
        });

        consumablesSlider.onValueChanged.AddListener((v) => {
            if (v > GetAmountLeft(3))
            {
                consumablesSlider.value = consumablesOldVal;
            }
            else
            {
                consumablesSlider.value = v;
            }

            float newVal = consumablesSlider.value;
            consumablesSliderAmountText.text = newVal.ToString("0");
            consumablesOldVal = consumablesSlider.value;
        });

        emergencySlider.onValueChanged.AddListener((v) => {
            if (v > GetAmountLeft(4))
            {
                emergencySlider.value = emergencyOldVal;
            }
            else
            {
                emergencySlider.value = v;
            }

            float newVal = emergencySlider.value;
            emergencySliderAmountText.text = newVal.ToString("0");
            emergencyOldVal = emergencySlider.value;
        });
    }


    private float GetAmountLeft(int index)
    {
        switch (index)
        {
            case 1:
                return (moneyValue - (savingsSlider.value + consumablesSlider.value + emergencySlider.value));
            case 2:
                return (moneyValue - (billsSlider.value + consumablesSlider.value + emergencySlider.value));
            case 3:
                return (moneyValue - (billsSlider.value + savingsSlider.value + emergencySlider.value));
            case 4:
                return (moneyValue - (billsSlider.value + savingsSlider.value + consumablesSlider.value));
            default:
                return 0;
        }
    }


    public void SaveBudget()
    {
        Debug.Log(int.Parse(billsSliderAmountText.text));
        Debug.Log(int.Parse(savingsSliderAmountText.text));
        Debug.Log(int.Parse(consumablesSliderAmountText.text));
        Debug.Log(int.Parse(emergencySliderAmountText.text));
    }
}

