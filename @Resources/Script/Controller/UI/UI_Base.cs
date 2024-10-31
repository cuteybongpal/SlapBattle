using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Base : BaseController
{
    Dictionary<string, Object> children = new Dictionary<string, Object>();
    protected T FindChild<T>(string name) where T : Component
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
}
