using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInput : MonoBehaviour
{
    //keys, actions���� ���� �ܺ� ��ũ��Ʈ���� �־���� �Ѵ�.
    public List<KeyCode> keys = new List<KeyCode>();
    public List<Action> actions = new List<Action>();

    private void Start()
    {
        uni_Update().Forget();
    }
    async UniTaskVoid uni_Update()
    {
        while (true)
        {
            for (int i =0; i < keys.Count; i++)
            {
                if (Input.GetKeyDown(keys[i]))
                    actions[i]?.Invoke();
            }
            await UniTask.Yield();
        }
    }
}
