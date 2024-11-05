using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    bool isChangingScene = false;
    int _currentSceneIndex;
    public int CurrentSceneIndex {  get { return _currentSceneIndex; } }
    public float Volume = 1f;
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
                    Time.timeScale = 1;
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
        AsyncOperation op = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);
        op.completed += (a) =>
        {
            Debug.Log("로딩 씬 번호 : "+(int)scene);
            AsyncOperation _op = SceneManager.UnloadSceneAsync(_currentSceneIndex);
            _op.completed += (a) =>
            {
                Debug.Log("언로딩 씬 번호 : " + _currentSceneIndex);
                _currentSceneIndex = (int)scene;
                GameStart();
            };
            while (_op.isDone)
                Debug.Log(_op.progress + "%");
        };
        while (op.isDone)
            Debug.Log(op.progress+"%");
    }
    async UniTask WaitForSeconds(float t)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(t));
    }
    void GameStart()
    {
        State = GameState.Play;
        Debug.Log("게임 시작");
        Managers.Resource.Instantiate("LeagueStart.prefab");
        
    }
}
