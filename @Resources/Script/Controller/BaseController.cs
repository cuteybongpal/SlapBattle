using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected async UniTask WaitForSeconds(float t)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(t));
    }
}
