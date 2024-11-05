using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance = null;
    public static Managers Instance
    {
        get
        {
            return instance;
        }
    }

    ObjectManager _object = new ObjectManager();
    ResourceManager _resources = new ResourceManager();
    DataManager _data = new DataManager();
    GameManager _game = new GameManager();
    EventManager _event = new EventManager();

    public static ObjectManager Object { get { return Instance._object; } }
    public static ResourceManager Resource { get { return Instance._resources; } }
    public static DataManager Data { get { return Instance._data; } }
    public static GameManager Game { get { return Instance._game; } }
    public static EventManager Event { get { return Instance._event; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Resource.LoadAllAsync<AudioClip>("PreLoadSound", () =>
            {
                Resource.LoadAllAsync<GameObject>("PreLoadPrefab", () =>
                {
                    Resource.LoadAllAsync<TextAsset>("PreLoadData", () =>
                    {
                        Data.Init();
                        Event.AssetAllLoaded?.Invoke();
                    });
                });
            });
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
