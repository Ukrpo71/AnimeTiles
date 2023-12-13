using System.Collections;
using UnityEngine;

namespace Common
{
    public class ItemLocalRotator : MonoBehaviour
    {
        [SerializeField] private Axis _axis;
        [SerializeField] private bool _autoStart;
        [SerializeField] private Transform _rotatable;
        [SerializeField] private float _rotAmount;

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (_rotatable == null)
                _rotatable = transform;
        }
        #endif

        private void OnEnable()
        {
            if (!_autoStart)
                return;
            Stop();
            Rotate();
        }

        private void OnDisable()
        {
            Stop();
        }

        public void Rotate()
        {
            switch (_axis)
            {
                case Axis.X:
                    StartCoroutine(RotatingX());
                    break;
                case Axis.Y:
                    StartCoroutine(RotatingY());
                    break;
                case Axis.Z:
                    StartCoroutine(RotatingZ());
                    break;
            }
        }

        public void Stop()
        {
            StopAllCoroutines();
        }

        private IEnumerator RotatingZ()
        {
            while (true)
            {
                var angles = _rotatable.localEulerAngles;
                angles.z += _rotAmount * Time.deltaTime;
                _rotatable.localEulerAngles = angles;
                yield return null;
            }
        }

        private IEnumerator RotatingY()
        {
            while (true)
            {
                var angles = _rotatable.localEulerAngles;
                angles.y += _rotAmount * Time.deltaTime;
                _rotatable.localEulerAngles = angles;
                yield return null;
            }
        }


        private IEnumerator RotatingX()
        {
            while (true)
            {
                var angles = _rotatable.localEulerAngles;
                angles.x += _rotAmount * Time.deltaTime;
                _rotatable.localEulerAngles = angles;
                yield return null;
            }
        }
        
        
        public enum Axis
        {
            Z,
            Y,
            X
        }
    }
}