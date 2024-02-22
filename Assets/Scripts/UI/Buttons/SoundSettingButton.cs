using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingButton : MonoBehaviour
{
    [SerializeField] private Sprite _withSound;
    [SerializeField] private Sprite _withoutSound;
    [SerializeField] private Image _settingImage;

    private void Start()
    {
        SetUp();
    }
    public void ToggleSound()
    {
        int sound = PlayerPrefs.GetInt("Sound");
        PlayerPrefs.SetInt("Sound", sound == 0 ? 1 : 0);
        SetUp();
    }

    private void SetUp()
    {
        bool withSound = PlayerPrefs.GetInt("Sound") == 0;
        if (withSound)
        {
            SoundManager.Instance.TurnOnSound();
            _settingImage.sprite = _withSound;
        }
        else
        {
            SoundManager.Instance.TurnOffSound();
            _settingImage.sprite = _withoutSound;
        }
    }
}
