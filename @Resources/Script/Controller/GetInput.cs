using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInput : MonoBehaviour
{
    //keys, actions들은 전부 외부 스크립트에서 넣어줘야 한다.
    List<Inputdatas> _inputdatas = new List<Inputdatas>();

    private void Start()
    {
        uni_Update().Forget();
    }
    async UniTaskVoid uni_Update()
    {
        while (true)
        {
            for (int i =0; i < _inputdatas.Count; i++)
            {
                if (_inputdatas[i].keyCode == KeyCode.None)
                {
                    if (!Input.anyKey)
                        _inputdatas[i].action?.Invoke();
                    continue;
                }
                switch (_inputdatas[i].clickType)
                {
                    case ClickType.Down:
                        if (Input.GetKeyDown(_inputdatas[i].keyCode))
                            _inputdatas[i].action?.Invoke();
                        break;
                    case ClickType.Pressed:
                        if (Input.GetKey(_inputdatas[i].keyCode))
                            _inputdatas[i].action?.Invoke();
                        break;
                }
                
            }
            await UniTask.Yield();
        }
    }
    public void Add(KeyCode key, Action action, ClickType type)
    {
        _inputdatas.Add(new Inputdatas(action,key, type));
    }
    public enum ClickType
    {
        Down,
        Pressed,
    }
    struct Inputdatas
    {
        public Action action;
        public KeyCode keyCode;
        public ClickType clickType;

        public Inputdatas(Action action, KeyCode keycode, ClickType type)
        {
            this.action = action;
            this.keyCode = keycode;
            this.clickType = type;
        }
    }
}
