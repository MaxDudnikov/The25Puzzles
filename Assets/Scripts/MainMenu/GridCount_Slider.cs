using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class GridCount_Slider : MonoBehaviour
{
    [Header("—сылка на скрипт пол€ ввода")]
    [SerializeField] private GridCount_Value GridCount_Value;

    internal UnityAction<int> OnValueChanged;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        GridCount_Value.OnValueChanged += SetValue;

        if (PlayerPrefs.HasKey("Grid_count"))
            LoadValue();

        _slider.onValueChanged.AddListener((float value) => SetValue(value));

    }

    private void SetValue(int value)
    {
        _slider.value = value;
    }

    private void SetValue(float value)
    {
        var result = Convert.ToInt32(value);
        OnValueChanged?.Invoke(result);
    }

    private void LoadValue()
    {
        _slider.value = PlayerPrefs.GetInt("Grid_count");
    }

    private void OnDisable()
    {
        GridCount_Value.OnValueChanged -= SetValue;
        _slider.onValueChanged.RemoveAllListeners();
    }
}
