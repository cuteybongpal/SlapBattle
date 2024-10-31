using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    bool isChangingScene = false;
    int _currentSceneIndex = 0;
    public int CurrentSceneIndex {  get { return _currentSceneIndex; } }
    public Define.Gloves CurrentGlove = Define.Gloves.Default;
    public enum GameState
    {
        Play,
        Slow,
        Pause
    }
    public enum Scene
    {
        Lobby,
        Game,
    }
    GameState _state = GameState.Play;
    Scene _scene = Scene.Lobby;
    public GameState State 
    {
        get { return _state; }
        set
        {
            _state = value;
            switch (_state)
            {
                case GameState.Play:
                    Time.timeScale = 0;
                    Time.fixedDeltaTime = Time.deltaTime;
                    break;
                case GameState.Slow:
                    Time.timeScale = 0.5f;
                    Time.fixedDeltaTime = Time.deltaTime * 0.5f;
                    break;
                case GameState.Pause:
                    Time.timeScale = 0; 
                    break;
            }
        }
    }
    public Scene CurrentScene
    {
        get { return _scene; }
        set
        {
            _scene = value;
            ChangeScene(_scene);
        }
    }
    async void ChangeScene(Scene scene)
    {
        while (isChangingScene)
            await WaitForSeconds(Time.deltaTime);
        Camera _main = Camera.main;
        await SceneManager.LoadSceneAsync((int)scene);
        await SceneManager.UnloadSceneAsync(_currentSceneIndex);
        _currentSceneIndex = (int)scene;
        if (_currentSceneIndex == (int)Scene.Game)
            GameStart();
    }
    async UniTask WaitForSeconds(float t)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(t));
    }
    void GameStart()
    {
        Managers.Object.Spawn<LeagueStart>("League.prfab");
    }
}
