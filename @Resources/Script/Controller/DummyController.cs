using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : CreatureController, ISubject
{
    List<IObserver> _observers = new List<IObserver>();
    public void Add(IObserver _observer)
    {
        _observers.Add(_observer);
    }

    public void Notify()
    {
        foreach (IObserver _observer in _observers)
        {
            _observer.OnNotified();
        }
    }

    public void Remove(IObserver _observer)
    {
        _observers.Remove(_observer);
    }

    void Start()
    {
        CurrentHp = MaxHp;
        UI_EnemyHPBar hpbar = Managers.Object.Spawn<UI_EnemyHPBar>("UI_EnemyHPBar.prefab");
        hpbar.Init(this);
        Add(hpbar);
        hpbar.transform.position = (Vector2)transform.position + Vector2.up * 1.5f;
        hpbar.transform.parent = transform;
    }

    public override void Damaged(int damge)
    {
        base.Damaged(damge);
        Debug.Log(damge);
        Notify();
    }
}
