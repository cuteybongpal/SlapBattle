using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager
{
    private PlayerController _player;
    private HashSet<MonsterController> _monsters = new HashSet<MonsterController>();
    private HashSet<UI_Base> _uis = new HashSet<UI_Base>();
    private List<MonsterController> _monsterPool = new List<MonsterController>();
    private List<UI_Base> _uiPool = new List<UI_Base>();
    private Transform _transform;

    public T Spawn<T>(string key) where T : BaseController
    {
        Type type = typeof(T);
        GameObject go = null;

        if (type == typeof(PlayerController))
        {
            go = Managers.Resource.Instantiate(key);
            PlayerController player = go.GetComponent<T>() as PlayerController;
            _player = player;
            Iinit _init = go.GetComponent<T>() as Iinit;
            _init.Init();
            return player as T;
        }
        else if (type == typeof(MonsterController))
        {
            if (_monsterPool.Count > 0)
                if (_monsterPool[0].IsUnityNull())
                    _monsterPool.Clear();
            if (_monsterPool.Count == 0)
                go = Managers.Resource.Instantiate(key);
            else
            {
                bool isTypeMatchFound = false;
                for (int i = 0; i < _uiPool.Count; i++)
                {
                    if (_uiPool[i].GetType() == typeof(T))
                    {
                        go = _monsterPool[i].gameObject;
                        isTypeMatchFound = true;
                        break;
                    }
                }
                if (!isTypeMatchFound)
                {
                    go = Managers.Resource.Instantiate(key);
                }
            }
            T controller = go.GetComponent<T>();
            Iinit _init = go.GetComponent<T>() as Iinit;
            _init.Init();
            _monsters.Add(controller as MonsterController);
            return controller as T;
        }
        else if (type.BaseType == typeof(UI_Base))
        {
            if (_uiPool.Count > 0)
                if (_uiPool[0].IsUnityNull())
                    _uiPool.Clear();
            if (_uiPool.Count == 0)
                go = Managers.Resource.Instantiate(key);
            else
            {
                bool isTypeMatchFound = false;
                for (int i = 0; i < _uiPool.Count; i++)
                {
                    if (_uiPool[i].GetType() == typeof(T))
                    {
                        go = _uiPool[i].gameObject;
                        isTypeMatchFound = true;
                        break;
                    }
                }
                if (!isTypeMatchFound)
                {
                    go = Managers.Resource.Instantiate(key);
                }
            }
            go.GetComponent<Iinit>().Init();
            T uiElement = go.GetComponent<T>();
            _uis.Add(uiElement as UI_Base);
            return uiElement as T;
        }
        return null;
    }

    public void DeSpawn<T>(T element) where T : BaseController
    {
        Type type = typeof(T);
        if (type == typeof(PlayerController))
        {
            _player.gameObject.SetActive(false);
            _player = null;
        }
        else if (type == typeof(MonsterController))
        {
            _monsters.Remove(element as MonsterController);
            element.gameObject.transform.position = Vector3.zero;
            element.gameObject.transform.parent = _transform;
            element.gameObject.SetActive(false);
            _monsterPool.Add(element as MonsterController);
        }
        else if (type == typeof(UI_Base))
        {
            _uis.Remove(element as UI_Base);
            element.gameObject.transform.parent = _transform;
            element.gameObject.SetActive(false);
            _uiPool.Add(element as UI_Base);
        }
    }
}
