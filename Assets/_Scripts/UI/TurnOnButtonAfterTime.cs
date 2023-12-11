using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TurnOnButtonAfterTime : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private float _timeToActivateButton = 1f;

    private void OnEnable()
    {
        _button.enabled = false;
        Invoke(nameof(ActivateButton), _timeToActivateButton);
    }

    private void ActivateButton()
    {
        _button.enabled = true;
    }
}
