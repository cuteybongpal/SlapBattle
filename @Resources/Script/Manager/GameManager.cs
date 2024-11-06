using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
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

    bool isChangingScene = false;
    int _currentSceneIndex;
    int _currentPunchAmount;
    public float Volume = 1f;
    public Define.Gloves CurrentGlove = Define.Gloves.Default;
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
    public int CurrentSceneIndex { get { return _currentSceneIndex; } }
    public int CurrentPunchAmount 
    {
        get {  return _currentPunchAmount; }
        set
        {
            _currentPunchAmount = value;
            Managers.Event.PunchInCrease?.Invoke(_currentPunchAmount);
        }
    }

    async void ChangeScene(Scene scene)
    {
        while (isChangingScene)
            await WaitForSeconds(Time.deltaTime);
        AsyncOperation op = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);
        op.completed += (a) =>
        {
            Debug.Log("�ε� �� ��ȣ : "+(int)scene);
            AsyncOperation _op = SceneManager.UnloadSceneAsync(_currentSceneIndex);
            _op.completed += (a) =>
            {
                Debug.Log("��ε� �� ��ȣ : " + _currentSceneIndex);
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
        //�ʱ�ȭ
        State = GameState.Play;
        Managers.Event.PlayerOnHit = null;
        Managers.Event.GameOver = null;
        Managers.Event.PunchInCrease = null;
        Debug.Log("���� ����");
        Managers.Resource.Instantiate("LeagueStart.prefab");
        
    }
}
