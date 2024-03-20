using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] private List<Button> _finishers;
    [Inject] private NewControls _control;

    private void OnEnable()
    {
        _control.UI.Exit.performed += OnEscapeClick;

        foreach (var button in _finishers)
            button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        foreach (var button in _finishers)
            button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Application.Quit();
    }

    private void OnEscapeClick(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
}
