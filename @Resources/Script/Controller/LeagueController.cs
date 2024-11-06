using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeagueController : BaseController
{
    PlayerController _player;
    UI_InGame InGameUI;
    int _survivedTime;
    AudioSource _audio;
    void Start()
    {
        _player = Managers.Object.Spawn<PlayerController>("Player.prefab");
        InGameUI = Managers.Object.Spawn<UI_InGame>("UI_InGame.prefab");
        _audio = GetComponent<AudioSource>();
        _audio.volume = Managers.Game.Volume;
        switch (Managers.Game.CurrentGlove)
        {
            case Define.Gloves.Default:
                _player.AddComponent<DefaultGlove>();
                break;
            case Define.Gloves.Energy:
                _player.AddComponent<EnergeGlove>();
                break;
        }
        Managers.Event.PlayerOnHit += (currentHP) => { InGameUI.RefreshHP(currentHP); };
        Managers.Event.PunchInCrease += (PunchAmount) => { InGameUI.RefreshPunch(PunchAmount); };
        _survivedTime = 0;
        StartCounting();
    }
    async void StartCounting()
    {
        while (true)
        {
            await WaitForSeconds(1);
            _survivedTime++;
            InGameUI.RefreshSurvivedTime(_survivedTime);
        }
    }
}