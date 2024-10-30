using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController, Iinit
{
    GetInput _input;
    private void Start()
    {

    }
    public new void Init()
    {
        _input = GetComponent<GetInput>();
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.MoveLeft], MoveLeft);
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.MoveRight], MoveRight);
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.Punch], Attack);
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.Jump], Jump);
    }
    void MoveLeft()
    {
        Debug.Log("PlayerMove Left");
    }
    void MoveRight()
    {
        Debug.Log("PlayerMove Right");
    }
    void Jump()
    {
        Debug.Log("PlayerJump");
    }
    void Attack()
    {
        Debug.Log("PlayerMoveAttack");
    }
}
