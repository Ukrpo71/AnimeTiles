using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnlockableImagePanel : MonoBehaviour
    {
        [SerializeField] private Image _iconFill;
        [SerializeField] private Image _iconBack;
        [SerializeField] private Text _progressText;
        [SerializeField] private AudioClip _fillingSound;
        [SerializeField] private ImageInventorySO _inventory;
        [SerializeField] private Text _label;
        [SerializeField] private float _timeToFill;
        [SerializeField] private float _timeAfterFill;

        public void Show()
        {
            // Debug.Log("shown");
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            // Debug.Log("hidden");
            gameObject.SetActive(false);
        }

        public void AddProgress(UnlockableImageInventoryData levelData, float from, float to, Action onEnd)
        {
            SoundManager.Instance.PlaySound(_fillingSound);
            _iconBack.sprite = _iconFill.sprite = levelData.Level.Image;
            StartCoroutine(Filling(from, to, onEnd));
        }

        public void AddProgress(Action onEnd, float increment = 0)
        {
            UnlockableImageInventoryData levelData = _inventory.GetNextImageToUnlock();
            if (_inventory.DoesNextImageExists())
            {
                levelData = _inventory.GetNextImageToUnlock();
            }
            else
            {
                onEnd.Invoke();
                Debug.Log("Next level to unlock doesn't exists");
                return;
            }

            var from = levelData.Progress;
            if (increment == 0) increment = levelData.Level.AmountToIncreaseProgress;
            var to = levelData.Progress + increment;
            _inventory.UpdateImageProgress(levelData, increment);
            SoundManager.Instance.PlaySound(_fillingSound);
            _iconBack.sprite = _iconFill.sprite = levelData.Level.Image;
            StartCoroutine(Filling(from, to, onEnd));

        }

        private IEnumerator Filling(float from, float to,  Action onEnd)
        {
            
            var elapsed = 0f;
            var time = _timeToFill;
            while (elapsed <= time)
            {
                var val = Mathf.Lerp(from, to, elapsed / time);
                SetValue(val);
                elapsed += Time.deltaTime;
                yield return null;
            }
            SetValue(to);
            yield return new WaitForSeconds(_timeAfterFill);
            onEnd?.Invoke();
            
            void SetValue(float val)
            {
                _iconFill.fillAmount = val;
                if (YandexManager.Instance.Language == "ru")
                    _progressText.text = $"{Mathf.RoundToInt(val * 100f)}% Разблокировано";
                else
                    _progressText.text = $"{Mathf.RoundToInt(val * 100f)}% Unlocked";
            }
        }        
    }
}