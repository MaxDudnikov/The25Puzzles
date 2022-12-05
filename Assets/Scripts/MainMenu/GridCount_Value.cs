using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_InputField))]
public class GridCount_Value : MonoBehaviour
{
    [Header("Минимальное и максимальное значение сетки")]
    [Tooltip("Значение этого поля должно совпадать с ограничениями слайдера снизу")]
    [SerializeField] private int min_value;
    [Tooltip("Значение этого поля должно совпадать с ограничениями слайдера сверху")]
    [SerializeField] private int max_value;

    [Space(10)]
    [Header("Ссылка на скрипт слайдера")]
    [SerializeField] private GridCount_Slider GridCount_Slider;

    private int _grid_count = 4;

    internal UnityAction<int> OnValueChanged;

    private TMP_InputField _inputField;

    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
    }

    private void OnEnable()
    {
        GridCount_Slider.OnValueChanged += SetValue;

        if (PlayerPrefs.HasKey("Grid_count"))
            LoadValue();

        _inputField.onEndEdit.AddListener((string value) => SetValue(value));

    }

    private void CheckCorrectnesValue(string str)
    {
        var value = Convert.ToInt32(str);

        if (value > max_value)
            _inputField.text = max_value.ToString();
        else if (value < min_value)
            _inputField.text = min_value.ToString();
        else
            _inputField.text = str;
    }

    private void SetValue(int value)
    {
        _inputField.text = value.ToString();

        _grid_count = Convert.ToInt32(_inputField.text);

        SaveValue(_grid_count);
    }

    private void SetValue(string str)
    {
        CheckCorrectnesValue(str);

        _inputField.text = str;

        _grid_count = Convert.ToInt32(_inputField.text);

        OnValueChanged?.Invoke(_grid_count);

        SaveValue(_grid_count);
    }

    private void SaveValue(int grid_count)
    {
        PlayerPrefs.SetInt("Grid_count", grid_count);
    }

    private void LoadValue()
    {
        _grid_count = PlayerPrefs.GetInt("Grid_count");
        _inputField.text = _grid_count.ToString();
    }

    private void OnDisable()
    {
        GridCount_Slider.OnValueChanged -= SetValue;
        _inputField.onEndEdit.RemoveAllListeners();
    }
}
