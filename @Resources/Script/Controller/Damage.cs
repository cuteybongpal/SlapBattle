using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    CreatureController _owner;
    int damage;
    event Action hitEvent;

    HashSet<CreatureController> hitCreature = new HashSet<CreatureController>();

    public void Init(CreatureController _owner, int damage, Action action = null)
    {
        this._owner = _owner;
        this.damage = damage;
        hitEvent += action;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(gameObject.tag))
            return;
        if (collision.CompareTag(_owner.gameObject.tag))
            return;
        if (collision.GetComponent<CreatureController>() == null)
            return;
        if (hitCreature.Contains(collision.GetComponent<CreatureController>()))
            return;
        CreatureController _controller = collision.GetComponent<CreatureController>();
        hitEvent?.Invoke();
        hitCreature.Add(_controller);
        _controller.Damaged(damage);
        if (_owner.CompareTag("Player"))
            Managers.Game.CurrentPunchAmount += 1;
    }
    private void OnEnable()
    {
        hitCreature.Clear();
    }
}
