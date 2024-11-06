using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : UI_Base
{
    Slider Slider_HP;
    Text Text_HP;
    Text Text_SurvivedTime;
    Text Text_Punch;

    public override void Init()
    {
        Slider_HP = FindChild<Slider>("Slider_HP");
        Slider_HP.maxValue = 100;
        Text_HP = FindChild<Text>("Text_HP");
        Text_Punch = FindChild<Text>("Text_Punch");
        Text_SurvivedTime = FindChild<Text>("Text_SurvivedTime");
        Text_Punch.text = $"ÆÝÄ¡¼ö : {Managers.Game.CurrentPunchAmount}";
        Slider_HP.value = 100;
    }

    public void RefreshHP(int currentHp)
    {
        Slider_HP.value = currentHp;
        Text_HP.text = $"{currentHp} / {Slider_HP.maxValue}";
    }
    public void RefreshPunch(int currentPunchAmount)
    {
        Text_Punch.text = $"ÆÝÄ¡¼ö : {currentPunchAmount}";
    }
    public void RefreshSurvivedTime(int t)
    {
        Text_SurvivedTime.text = $"{t}ÃÊ";
    }
}
