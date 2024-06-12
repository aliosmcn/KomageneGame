using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/FloatEvent", fileName = "newFloatEvent")]

public class FloatEvent : ScriptableObject
{
    private Action<float> _action;

    public void AddListener(Action<float> action)
    {
        this._action += action;
    }

    public void RemoveListener(Action<float> action)
    {
        this._action -= action;
    }

    public void Raise(float value)
    {
        this._action?.Invoke(value);
    }
}
