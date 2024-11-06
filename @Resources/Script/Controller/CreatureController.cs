using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : BaseController
{
    public int MaxHp;
    int _currentHp;

    public enum CreatureState
    {
        Idle,
        Move,
        Damaged,
        Attack
    }
    CreatureState _state = CreatureState.Idle;
    protected CreatureState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            switch (_state)
            {
                case CreatureState.Idle:
                    S_Idle();
                    break;
                case CreatureState.Move:
                    S_Move();
                    break;
                case CreatureState.Attack:
                    S_Attack();
                    break;
                default:
                    break;
            }
        }
    }
    public int CurrentHp { get { return _currentHp; }  protected set { _currentHp = value; } }

    protected virtual void S_Idle()
    {

    }
    protected virtual void S_Move()
    {

    }
    protected virtual void S_Attack()
    {

    }
    protected virtual void S_Damaged()
    {

    }
    public virtual void Damaged(int damge)
    {
        if (CurrentHp <= 0)
            return;
        _currentHp -= damge;
        if (CurrentHp <= 0)
            Die();
    }
    protected virtual void Die()
    {

    }
}
