using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : BaseController
{
    public int MaxHp;
    public int CurrentHp;

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
    public void Damaged(int damge)
    {
        if (CurrentHp <= 0)
            return;
        CurrentHp -= damge;
        if (CurrentHp <= 0)
            Die();
    }
    protected virtual void Die()
    {

    }
}
