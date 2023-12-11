using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndRoot : MonoBehaviour
{
    [SerializeField] private WinScreen _winscreen;
    [SerializeField] private LoseScreen _loseScreen;
    [SerializeField] private Board _board;
    [SerializeField] private FloatVariable _money;
    [SerializeField] private float _moneyForLevel;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _winSound;

    private bool _loseScreenActive, _winScreenActive;

    private void Awake()
    {
        _board.GameOver += ShowLoseScreen;
        _board.GameWon += HandleGameWon;
    }

    public void HandleGameWon()
    {
        if (_winScreenActive) return;
        
        ShowWinScreen();
        Invoke(nameof(AddMoneyForLevelCompletion), 0.5f);
        Invoke(nameof(SaveLevel), 0.55f);
        _winScreenActive = true;
    }

    private void SaveLevel()
    {
        DataManager.Instance.SaveAsIfNextLevelCompleted();
    }

    private void AddMoneyForLevelCompletion()
    {
        _money.Value += _moneyForLevel;
    }

    public void ShowWinScreen()
    {
        _winscreen.Show();
        SoundManager.Instance.PlaySound(_winSound);
    }

    public void HideWinScreen()
    {
        if (_winScreenActive == false) return;
        _winscreen.Hide();
        _winScreenActive = false;
    }

    public void ShowLoseScreen()
    {
        if (_loseScreenActive) return;

        SoundManager.Instance.PlaySound(_loseSound);
        _loseScreen.Show();
        _loseScreenActive = true;
    }

    public void HideLoseScreen()
    {
        if (_loseScreenActive == false) return;

        _loseScreen.Hide();
        _loseScreenActive = false;
    }
}
