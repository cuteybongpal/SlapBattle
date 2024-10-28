using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : BaseController
{
    public enum CreatureState
    {
        Idle,
        Move,
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
}
