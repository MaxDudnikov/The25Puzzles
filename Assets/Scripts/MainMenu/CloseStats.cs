using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CloseStats : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() => Close());
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private void Close()
    {
        _animator.SetTrigger("CloseIt");
    }
}
