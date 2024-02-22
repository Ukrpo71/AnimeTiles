using Agava.WebUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionsHandler : PersistentSingleton<SessionsHandler>
{
    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += ToggleSession;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= ToggleSession;
    }

    private void ToggleSession(bool inBackground)
    {
        if (inBackground)
        {
            GameAnalyticsSDK.GameAnalytics.EndSession();
            GameAnalyticsSDK.GameAnalytics.NewDesignEvent("SessionEnded");
            OnOutOfFocus();
        }
        else
        {
            GameAnalyticsSDK.GameAnalytics.StartSession();
            GameAnalyticsSDK.GameAnalytics.NewDesignEvent("SessionStart");
            OnReturnToFocus();
        }
    }

    public void OnClose()
    {
        GameAnalyticsSDK.GameAnalytics.EndSession();
        GameAnalyticsSDK.GameAnalytics.NewDesignEvent("SessionEnded");
    }

    private void OnReturnToFocus()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetInt("AdsShowing") == 1)
            return;
        AudioListener.pause = false;
        AudioListener.volume = 1;
    }

    private void OnOutOfFocus()
    {
        AudioListener.pause = true;
        AudioListener.volume = 0;
        Time.timeScale = 0;
    }
}
