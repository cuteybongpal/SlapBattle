using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : CreatureController, Iinit, ISubject
{
    bool _canMove;
    bool _canAttack = true;
    bool _canJump = true;
    GetInput _input;
    Animator _anim;
    SpriteRenderer _renderer;
    Rigidbody2D _rbody;
    public GloveController _glove;
    float _punchCoolDown = .5f;
    Damage Punch;
    AudioClip _walking;
    AudioSource _audio;
    List<IObserver> _objservers = new List<IObserver>();

    public new void Init()
    {
        _canMove = true;
        _canJump = true;
        _input = GetComponent<GetInput>();
        _input.Add(Managers.Data.KeyBinds[Define.KeyEvents.MoveLeft], MoveLeft, GetInput.ClickType.Pressed);
        _walking = Managers.Resource.Load<AudioClip>("Walking.mp3");
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
        _audio = GetComponent<AudioSource>();
        _audio.volume = Managers.Game.Volume;
        Punch = transform.GetChild(0).GetComponent<Damage>();
        Punch.ownerTag = gameObject.tag;
        Punch.damage = _glove.Attack;
        Punch.gameObject.SetActive(false);
        CheckCanJump();
    }
    #region Ojserver Pattern
    public void Add(IObserver _observer)
    {
        _objservers.Add(_observer);
    }
    public void Remove(IObserver _observer)
    {
        _objservers.Remove(_observer);
    }
    public void Notify()
    {
        foreach(IObserver _ob in  _objservers)
        {
            _ob.OnNotified();
        }
    }
    #endregion
    #region input Method
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
        if (_canJump)
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
    #endregion
    #region State Method
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
    #endregion
    protected override void Die()
    {

    }
    async void CheckCanJump()
    {
        while (true)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 1.2f);
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("BackGround"))
                {
                    _canJump = true;
                    break;
                }
                _canJump = false;
            }
            await WaitForSeconds(Time.deltaTime);
        }
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
    void WalkSound()
    {
        _audio.PlayOneShot(_walking);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            Damage damage = collision.GetComponent<Damage>();
            if (damage.ownerTag.Equals(gameObject.tag))
                return;
            else
            {

            }
        }
    }
}
