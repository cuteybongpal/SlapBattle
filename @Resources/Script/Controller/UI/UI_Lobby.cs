using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : UI_Base
{
        
    void Start()
    {
        FindChild<Button>("Button_DefaultGlove").onClick.AddListener(() =>
        {
            Managers.Game.CurrentGlove = Define.Gloves.Default;
        });
        FindChild<Button>("Button_EnergeGlove").onClick.AddListener(() =>
        {
            Managers.Game.CurrentGlove = Define.Gloves.Energy;
        });
        FindChild<Button>("Button_GameStart").onClick.AddListener(() =>
        {
            Managers.Game.CurrentScene = GameManager.Scene.Game;
        });
    }

    void Update()
    {
        
    }
}
