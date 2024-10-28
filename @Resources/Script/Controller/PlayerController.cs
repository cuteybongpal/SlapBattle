using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController, Iinit
{
    GetInput _input;
    private void Start()
    {
        _input = GetComponent<GetInput>();
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.MoveLeft], MoveLeft);
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.MoveRight], MoveRight);
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.Punch], Attack);
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.Jump], Jump);
    }
    public void Init()
    {

    }
    void MoveLeft()
    {

    }
    void MoveRight()
    {

    }
    void Jump()
    {

    }
    void Attack()
    {

    }
}
