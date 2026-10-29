using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySomeonesHP : MonoBehaviour
{
    [SerializeField] private HPManage TargetHPManage;
    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = TargetHPManage.MaxHP;
    }

    void Update()
    {
        slider.value = TargetHPManage.CurrentHP;
    }
}
