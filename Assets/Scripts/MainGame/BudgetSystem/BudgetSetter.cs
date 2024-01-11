using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BudgetSetter : MonoBehaviour
{
    [SerializeField] private List<Slider> sliders = new List<Slider>();
    [SerializeField] private List<Slider> recommSliders = new List<Slider>();
    [SerializeField] private List<TextMeshProUGUI> amountTexts = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI playerCurrentMoneyText;
    [SerializeField] private GameObject budgetSetterPopUp;
    private const float BillsRecommPercentage = 0.5f;
    private const float SavingsRecommPercentage = 0.1f;
    private const float ConsumablesRecommPercentage = 0.3f;
    private const float EmergencyRecommPercentage = 0.1f;
    private float[] oldVals = new float[4];
    private float moneyValue;


    public void PrepareBudgeSetter(float currentPlayerMoney)
    {
        LevelManager.Instance.CameraMovementRef.enabled = false;

        if (currentPlayerMoney == -1f)
        {
            Player.Instance.PlayerCash = 5000f;
            currentPlayerMoney = 5000f;
        }

        this.gameObject.SetActive(true);
        OverlayAnimations.Instance.AnimOpenOverlay(budgetSetterPopUp);

        playerCurrentMoneyText.text = currentPlayerMoney.ToString();
        moneyValue = currentPlayerMoney;

        for(int i = 0; i < sliders.Count; i++)
        {
            oldVals[i] = 0f;
            sliders[i].maxValue = currentPlayerMoney;
            recommSliders[i].maxValue = currentPlayerMoney;
            amountTexts[i].text = sliders[i].value.ToString("0");

            switch (i)
            {
                case 0:
                    recommSliders[i].value = currentPlayerMoney * BillsRecommPercentage;
                    sliders[i].value = currentPlayerMoney * BillsRecommPercentage;
                    amountTexts[i].text = (currentPlayerMoney * BillsRecommPercentage).ToString("0");
                    break;
                case 1:
                    recommSliders[i].value = currentPlayerMoney * SavingsRecommPercentage;
                    sliders[i].value = currentPlayerMoney * SavingsRecommPercentage;
                    amountTexts[i].text = (currentPlayerMoney * SavingsRecommPercentage).ToString("0");
                    break;
                case 2:
                    recommSliders[i].value = currentPlayerMoney * ConsumablesRecommPercentage;
                    sliders[i].value = currentPlayerMoney * ConsumablesRecommPercentage;
                    amountTexts[i].text = (currentPlayerMoney * ConsumablesRecommPercentage).ToString("0");
                    break;
                case 3:
                    recommSliders[i].value = currentPlayerMoney * EmergencyRecommPercentage;
                    sliders[i].value = currentPlayerMoney * EmergencyRecommPercentage;
                    amountTexts[i].text = (currentPlayerMoney * EmergencyRecommPercentage).ToString("0");
                    break;
            }
        }

        //bills
        sliders[0].onValueChanged.AddListener((v) => { UpdateSlider(0, v); });

        //savings
        sliders[1].onValueChanged.AddListener((v) => { UpdateSlider(1, v); });

        //consumables
        sliders[2].onValueChanged.AddListener((v) => { UpdateSlider(2, v); });

        //emergency
        sliders[3].onValueChanged.AddListener((v) => { UpdateSlider(3, v); });
    }


    private void UpdateSlider(int sliderIndex, float value)
    {
        if (value > GetAmountLeft(sliderIndex))
        {
            sliders[sliderIndex].value = oldVals[sliderIndex];
        }
        else
        {
            sliders[sliderIndex].value = value;
        }

        float newVal = sliders[sliderIndex].value;
        amountTexts[sliderIndex].text = newVal.ToString("0");
        oldVals[sliderIndex] = sliders[sliderIndex].value;
    }


    private float GetAmountLeft(int index)
    {
        float amountLeft = moneyValue;

        for (int i = 0; i < sliders.Count; i++)
        {
            if (i != index)
            {
                amountLeft -= sliders[i].value;
            }
        }

        return amountLeft;
    }


    public void Continue()
    {
        LevelManager.Instance.CameraMovementRef.enabled = true;
        AudioManager.Instance.PlaySFX("Select");

        if (sliders[0].value + sliders[1].value + sliders[2].value + sliders[3].value < (moneyValue * 0.9))
        {
            return;
        }

        BudgetSystem.Instance.SaveBudget(sliders[0].value, sliders[1].value, sliders[2].value, sliders[3].value);
        this.gameObject.SetActive(false);
        OverlayAnimations.Instance.AnimCloseOverlay(budgetSetterPopUp, this.gameObject);
        TutorialManager.Instance.IsBudgetSet = true;
        // TutorialManager.Instance.StartTutorial();

        if (PlayerPrefs.GetInt("GameStart") == 0)
        {
            PlayerPrefs.SetInt("GameRestart", 1);
            GameManager.Instance.StartGame(new GameStateData()); 
        }
        else
        {
            GameManager.Instance.StartNextLevel(); //continue next level
        }
    }
}
