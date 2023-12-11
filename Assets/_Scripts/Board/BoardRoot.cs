using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRoot : MonoBehaviour
{
    [SerializeField] private LevelsHolder _levelsHolder;
    [SerializeField] private LevelLoader _levelsLoader;
    [SerializeField] private Board _board;
    [SerializeField] private Canvas _canvasTransform;
    [SerializeField] private GameEndRoot _gameEndRoot;
    [SerializeField] private Queue _queue;
    [SerializeField] private IntVariable _currentLevel;

    public void StartBoard()
    {
        var level = _levelsHolder.GetLevelByIndex(_currentLevel.Value);
        _levelsLoader.LoadLevel(level);
        _board.Init(_levelsLoader.CurrentLevel.Tiles);
    }

    public void ReloadLevel()
    {
        _levelsLoader.ReloadCurrentLevel();
        _board.Init(_levelsLoader.CurrentLevel.Tiles);
        _gameEndRoot.HideLoseScreen();
        _gameEndRoot.HideWinScreen();
        _queue.FreeQueue();
    }

    public void LoadNextLevel()
    {
        _currentLevel.Value += 1;
        var level = _levelsHolder.GetLevelByIndex(_currentLevel.Value);
        _levelsLoader.LoadLevel(level);
        _board.Init(_levelsLoader.CurrentLevel.Tiles);
        _gameEndRoot.HideLoseScreen();
        _gameEndRoot.HideWinScreen();
        _queue.FreeQueue();
    }
}
