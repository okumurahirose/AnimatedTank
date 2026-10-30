using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySomeonesHP : MonoBehaviour
{
    [SerializeField] private CharacterBase TargetCharacterBase;
    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = TargetCharacterBase.CopyMaxHP;
    }

    void Update()
    {
        slider.value = TargetCharacterBase.CopyCurrentHP;
    }
}
