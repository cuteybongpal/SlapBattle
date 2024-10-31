using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController, Iinit
{
    bool _canMove;
    GetInput _input;
    Animator _anim;
    SpriteRenderer _renderer;
    Rigidbody2D _rbody;
    public GloveController _glove;

    public new void Init()
    {
        _canMove = true;
        _input = GetComponent<GetInput>();
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.MoveLeft], MoveLeft, GetInput.ClickType.Pressed);

        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.MoveRight], MoveRight, GetInput.ClickType.Pressed);
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.Punch], Attack, GetInput.ClickType.Down);
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.None], Idle, GetInput.ClickType.Pressed);
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.Jump], Jump, GetInput.ClickType.Down);
        _glove = GetComponent<GloveController>();
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _rbody = GetComponent<Rigidbody2D>();
    }
    void MoveLeft()
    {
        if (!_canMove)
            return;
        _renderer.flipX = true;
        _rbody.velocity = new Vector2(-_glove.Speed, _rbody.velocity.y);
        State = CreatureState.Move;
    }
    void MoveRight()
    {
        if (!_canMove)
            return;
        _renderer.flipX = false;
        _rbody.velocity = new Vector2(_glove.Speed, _rbody.velocity.y);
        State = CreatureState.Move;
    }
    void Jump()
    {
        _rbody.velocity = new Vector2(_rbody.velocity.x, 5);
    }
    void Attack()
    {
        State = CreatureState.Attack;
        DisableMove(0.167f).Forget();
    }
    void Idle()
    {
        _rbody.velocity = new Vector2(0, _rbody.velocity.y);
        State = CreatureState.Idle;
    }
    protected override void S_Attack()
    {
        _anim.Play("PlayerAttack");
    }
    protected override void S_Move()
    {
        _anim.Play("PlayerRun");
    }
    protected override void S_Idle()
    {
        _anim.Play("PlayerIdle");
    }
    async UniTaskVoid DisableMove(float t)
    {
        _canMove = false;
        _rbody.velocity = new Vector2(0, _rbody.velocity.y);
        await UniTask.Delay(TimeSpan.FromSeconds(t));
        _canMove = true;
    }
    protected override void Die()
    {
        
    }
}
