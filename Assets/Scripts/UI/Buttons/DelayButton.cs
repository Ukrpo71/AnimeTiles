using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    private void Awake()
    {
        _button.onClick.AddListener(DelayButtonMethod);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(DelayButtonMethod);
    }

    private void OnDisable()
    {
        _button.enabled = true;
    }
    public void DelayButtonMethod()
    {
        Debug.Log("DelayingButton");
        if (gameObject.activeInHierarchy)
            StartCoroutine(StopButton());
    }
    IEnumerator StopButton()
    {
        _button.enabled = false;
        yield return new WaitForSeconds(1f);
        _button.enabled = true;
    }
}
