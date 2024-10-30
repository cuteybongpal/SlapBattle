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

    public static ObjectManager Object { get { return Instance._object; } }
    public static ResourceManager Resource { get { return Instance._resources; } }
    public static DataManager Data { get { return Instance._data; } }
    public static GameManager Game { get { return Instance._game; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Resource.LoadAllAsync<TextAsset>("PreLoadData", () =>
            {
                Data.Init();
                Resource.LoadAllAsync<GameObject>("PreLoadPrefab", () =>
                {
                    Object.Spawn<PlayerController>("Player.prefab");
                });
            });
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
