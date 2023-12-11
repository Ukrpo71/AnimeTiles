using UnityEngine;
using UnityEngine.UI;

public class CurrentLevelText : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private IntVariable _currentLevel;

    private void OnEnable()
    {
        _currentLevel.OnValueChanged += HandleLevelChanged;
        HandleLevelChanged(_currentLevel.Value);
    }

    private void OnDisable()
    {
        _currentLevel.OnValueChanged -= HandleLevelChanged;
    }

    private void HandleLevelChanged(int level)
    {
        var localizedText = YandexManager.Instance.Language == "ru" ? "Уровень " : "Level ";
        _text.text = localizedText + (level+1);
    }
}
