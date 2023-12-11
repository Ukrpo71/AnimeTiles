using UnityEngine;
using UnityEngine.UI;

public class LevelFinishedText :MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private IntVariable _currentLevel;

    private void OnEnable()
    {
        int level = _currentLevel.Value + 1;
        var localizedString = YandexManager.Instance.Language == "ru" ? $"Уровень {level} пройден" : $"Level {level} Finished";
        _text.text = localizedString;
    }
}
