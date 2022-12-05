using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class MaterialBlocks : MonoBehaviour
{
    private TMP_Dropdown _TMP_Dropdown;

    private void Awake()
    {
        _TMP_Dropdown = GetComponent<TMP_Dropdown>();
    }
    private void OnEnable()
    {
        LoadValue();

        _TMP_Dropdown.onValueChanged.AddListener((int value) => SaveValue(value));
    }

    private void LoadValue()
    {
        if (PlayerPrefs.HasKey("Material"))
            _TMP_Dropdown.value = PlayerPrefs.GetInt("Material");
    }

    private void SaveValue(int value)
    {
        PlayerPrefs.SetInt("Material", value);
    }

    private void OnDisable()
    {
        _TMP_Dropdown.onValueChanged.RemoveAllListeners();
    }
}
