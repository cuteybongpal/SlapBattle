using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance = null;
    ObjectManager _object = new ObjectManager();
    ResourceManager _resources = new ResourceManager();
    public static Managers Instance {
        get {
            return instance;
        }
    }
    public static ObjectManager Object { get { return Instance._object; } }
    public static ResourceManager Resource { get { return Instance._resources; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
