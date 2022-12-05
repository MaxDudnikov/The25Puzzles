using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Settings : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() => OnShowSettings());
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private void OnShowSettings()
    {
        _animator.SetTrigger("OpenIt");
    }
}
