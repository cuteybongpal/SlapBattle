using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeagueController : BaseController
{
    PlayerController _player;
    void Start()
    {
        _player = Managers.Object.Spawn<PlayerController>("Player.prefab");
        switch (Managers.Game.CurrentGlove)
        {
            case Define.Gloves.Default:
                _player.AddComponent<DefaultGlove>();
                break;
            case Define.Gloves.Energy:
                break;
        }
    }
    void Update()
    {
        
    }
}
