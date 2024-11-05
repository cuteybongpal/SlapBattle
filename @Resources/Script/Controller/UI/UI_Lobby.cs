using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : UI_Base
{
    Slider _sliderSoundControl;
    AudioSource _audio;
    void Start()
    {
        Managers.Event.AssetAllLoaded += AssetAllLoaded;
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
        _sliderSoundControl = FindChild<Slider>("Slider_SoundControl");
        _sliderSoundControl.value = Managers.Game.Volume;
        _sliderSoundControl.onValueChanged.AddListener((value) =>
        {
            _audio.volume = value;
            Managers.Game.Volume = value;
        });
        
    }

    void AssetAllLoaded()
    {
        FindChild<Image>("Image_Loading").gameObject.SetActive(false);
        _audio = GetComponent<AudioSource>();
        _audio.clip = Managers.Resource.Load<AudioClip>("BGM-Lobby.mp3");
        _audio.volume = Managers.Game.Volume;
        _audio.Play();

    }
}
