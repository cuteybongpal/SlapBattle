using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    public void OnNotified<T>(T arguement);
}
