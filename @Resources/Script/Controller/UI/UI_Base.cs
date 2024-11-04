using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Base : BaseController
{
    Dictionary<string, UnityEngine.Object> children = new Dictionary<string, UnityEngine.Object>();
    protected T FindChild<T>(string name) where T : MonoBehaviour
    {
        if (children.ContainsKey(name))
            return children[name] as T;
        T[] components = GetComponentsInChildren<T>();
        foreach (T component in components)
        {
            if (component.name == name)
                return component;
        }
        return null;
    }
    protected async UniTask WaitforSeconds(float seconds)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(seconds));
    }
}
