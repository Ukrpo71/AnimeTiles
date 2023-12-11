﻿using System;
using UnityEngine;

public class ScriptableObjectVariable<T> : ScriptableObject
{
    [SerializeField] private T _value;

    public T Value
    {
        get { return _value; }
        set
        {
            _value = value;
            OnValueChanged?.Invoke(value);
        }
    }

    public event Action<T> OnValueChanged;
}
