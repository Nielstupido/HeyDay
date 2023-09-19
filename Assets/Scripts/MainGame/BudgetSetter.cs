using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BudgetSetter : MonoBehaviour
{
    [SerializeField] private Slider SliderUi;
    [SerializeField] private TextMeshProUGUI SliderText;

    // Start is called before the first frame update
    void Start()
    {
        SliderUi.onValueChanged.AddListener((v) => {
            SliderText.text = v.ToString("0");
        });

    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

