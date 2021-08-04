using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    #region Events

    public delegate void OnGameStateHandler();
    public event OnGameStateHandler Win;
    public event OnGameStateHandler Lose;
    public event OnGameStateHandler StartRun;

    #endregion

    private SaverLoader _saverLoader;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        _saverLoader = SaverLoader.GetInstance();
        _saverLoader.LevelText = 0;
        StartGame();
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    private void StartGame()
    {
        StartRun?.Invoke();
    }
    
    public void EndGame()
    {
        Lose?.Invoke();
    }

    public void WinGame()
    {

        Win?.Invoke();
        SceneManager.LoadScene(1);
    }

    public void TryAgain()
    {
        _saverLoader.LevelText = 0;
        SceneManager.LoadScene(1);
    }
}
