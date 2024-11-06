using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyHPBar : UI_Base, IObserver
{
    CreatureController _owner;
    Slider Slider_hpBar;
    public void Init(CreatureController _owner)
    {
        this._owner = _owner;
        Slider_hpBar = FindChild<Slider>("Slider_HPBar");
        Slider_hpBar.maxValue = _owner.MaxHp;
        Slider_hpBar.value = _owner.CurrentHp;
    }
    public void OnNotified()
    {
        Slider_hpBar.value = _owner.CurrentHp;
    }
}
