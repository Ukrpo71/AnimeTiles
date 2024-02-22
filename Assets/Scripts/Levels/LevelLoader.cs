using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Level CurrentLevel => _initializedLevel;

    [SerializeField] private Transform _levelParent;

    private Level _initializedLevel;
    private Level _lastLoadedLevel;

    public void LoadLevel(Level level)
    {
        if (_initializedLevel != null)
            Destroy(_initializedLevel.gameObject);

        _lastLoadedLevel = level;
        _initializedLevel = Instantiate(level, _levelParent.transform);
        _initializedLevel.Init();
    }

    public void ReloadCurrentLevel()
    {
        Destroy(_initializedLevel.gameObject);
        LoadLevel(_lastLoadedLevel);
    }
}
