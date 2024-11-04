using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : CreatureController, Iinit
{
    bool _canMove;
    bool _canAttack = true;
    GetInput _input;
    Animator _anim;
    SpriteRenderer _renderer;
    Rigidbody2D _rbody;
    public GloveController _glove;
    float _punchCoolDown = .5f;
    Damage Punch;

    public new void Init()
    {
        _canMove = true;
        _input = GetComponent<GetInput>();
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.MoveLeft], MoveLeft, GetInput.ClickType.Pressed);

        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.MoveRight], MoveRight, GetInput.ClickType.Pressed);
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.Punch], Attack, GetInput.ClickType.Down);
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.None], Idle, GetInput.ClickType.Pressed);
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.Jump], Jump, GetInput.ClickType.Down);
        switch (Managers.Game.CurrentGlove)
        {
            case Define.Gloves.Default:
                gameObject.AddComponent<DefaultGlove>();
                break;
            case Define.Gloves.Energy:
                gameObject.AddComponent<EnergeGlove>();
                break;
        }
        _glove = GetComponent<GloveController>();
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _rbody = GetComponent<Rigidbody2D>();
        Punch = transform.GetChild(0).GetComponent<Damage>();
        Punch.ownerTag = gameObject.tag;
        Punch.damage = _glove.Attack;
        Punch.gameObject.SetActive(false);
    }
    void MoveLeft()
    {
        if (!_canMove || State == CreatureState.Damaged)
            return;
        transform.rotation = Quaternion.Euler(0, 0, 180f);
        _renderer.flipY = true;
        _rbody.velocity = new Vector2(-_glove.Speed, _rbody.velocity.y);
        State = CreatureState.Move;
    }
    void MoveRight()
    {
        if (!_canMove || State == CreatureState.Damaged)
            return;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _renderer.flipY = false;
        _rbody.velocity = new Vector2(_glove.Speed, _rbody.velocity.y);
        State = CreatureState.Move;
    }
    void Jump()
    {
        _rbody.velocity = new Vector2(_rbody.velocity.x, 5);
    }
    async void Attack()
    {
        if (!_canAttack)
            return;

        State = CreatureState.Attack;
        DisableMove(0.167f).Forget();
        DisableAttack().Forget();

        Punch.gameObject.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(0.167f));
        Punch.gameObject.SetActive(false);
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
    protected override void Die()
    {

    }
    async UniTaskVoid DisableMove(float t)
    {
        _canMove = false;
        _rbody.velocity = new Vector2(0, _rbody.velocity.y);
        await UniTask.Delay(TimeSpan.FromSeconds(t));
        _canMove = true;
    }
    async UniTaskVoid DisableAttack()
    {
        _canAttack = false;
        await UniTask.Delay(TimeSpan.FromSeconds(_punchCoolDown));
        _canAttack = true;
    }
    
}
