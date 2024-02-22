using UnityEngine;
using UnityEngine.UI;

public class NextLevelText : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private IntVariable _currentLevel;

    private void OnEnable()
    {
        int level = _currentLevel.Value + 2;
        var localizedString = YandexManager.Instance.Language == "ru" ? $"Уровень {level}" : $"Level {level}";
        _text.text = localizedString;
    }
}
