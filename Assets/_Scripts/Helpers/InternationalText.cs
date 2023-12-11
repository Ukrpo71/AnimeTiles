using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternationalText : MonoBehaviour
{

    [SerializeField] string _ru;
    [SerializeField] string _en;

    private void Start()
    {
        if (YandexManager.Instance == null)
        {
            if (TryGetComponent(out Text normalText))
                normalText.text = _ru;
            return;
        }
        if (YandexManager.Instance.Language == "com")
        {
            if (TryGetComponent(out Text normalText))
                normalText.text = _en;
        }
        else if (YandexManager.Instance.Language == "ru")
        {
            if (TryGetComponent(out Text normalText))
                normalText.text = _ru;
        }
        else
        {
            if (TryGetComponent(out Text normalText))
                normalText.text = _en;
        }
    }

}
