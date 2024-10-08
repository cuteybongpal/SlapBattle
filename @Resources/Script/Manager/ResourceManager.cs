using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager
{
    Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();

    public T Load<T>(string key) where T : UnityEngine.Object
    {
        if (_resources.ContainsKey(key))
        {
            return _resources[key] as T;
        }
        return null;
    }

    public void LoadAsync<T>(string key, Action<bool> callback = null) where T : UnityEngine.Object
    {
        if (_resources.ContainsKey(key))
        {
            callback.Invoke(true);
            return;
        }
        else
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
            handle.Completed += (result) =>
            {
                if (result.Status == AsyncOperationStatus.Succeeded)
                {
                    _resources.Add(key, (UnityEngine.Object)handle.Result);
                    callback.Invoke(true);
                }
                else
                { 
                    callback.Invoke(false);
                }
            };
        }
    }
    public void LoadAllAsync<T>(string label, Action callback = null) where T : UnityEngine.Object
    {
        var handle = Addressables.LoadResourceLocationsAsync(label, typeof(T));

        handle.Completed += (loadedAsset) =>
        {
            if (loadedAsset.Status == AsyncOperationStatus.Succeeded)
            {
                bool[] LoadedArray = new bool[loadedAsset.Result.Count];
                for (int i = 0; i < loadedAsset.Result.Count; i++)
                {
                    LoadAsync<T>(loadedAsset.Result[i].PrimaryKey, (bool isload) =>
                    {
                        LoadedArray[i] = isload;
                        bool isLoadAll = false;
                        for (int j = 0; j < LoadedArray.Length; j++)
                            isLoadAll &= LoadedArray[j];
                        if (isLoadAll)
                            callback.Invoke();
                    });
                }
            }
            else
            {
                Debug.Log("AsyncLoadingFailed");
            }
        };
    }
}
