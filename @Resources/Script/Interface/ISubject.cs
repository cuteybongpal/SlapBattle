using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    public void Add(IObserver _observer);
    public void Remove(IObserver _observer);
    public void Notify();
}
