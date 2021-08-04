using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private GameObject _gameWindow;
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private Text _completedLevel;
    [SerializeField] private Text _endCompletedLevel;
    [SerializeField] private Text _pauseCompletedLevel;
    [SerializeField] private LeanLocalization _lean;
    [SerializeField] private Text _coinsText;
    [SerializeField] private Button _soundButtonOn;
    [SerializeField] private Button _soundButtonOff;
    [SerializeField] private Button _engButton;
    [SerializeField] private Button _deuButton;
    [SerializeField] private GameState _gameState;
    [SerializeField] private Color _offedColor;
    [SerializeField] private Color _normalColor;
    
    private SaverLoader _saverLoader;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Invoke(nameof(Initialize), 0.1f);
    }
    private void Initialize()
    {
        _saverLoader = SaverLoader.GetInstance();
        Subscribe();
        UpdateOptions();
        ShowBestRecord();
        SceneManager.LoadScene(1);
    }

    public void UpdateSound(bool isOn)
    {
        if (isOn)
        {
            _soundButtonOff.targetGraphic.color = _offedColor;
            _soundButtonOn.targetGraphic.color = _normalColor;
            AudioListener.volume = 1;
        }
        else
        {
            _soundButtonOff.targetGraphic.color = _normalColor;
            _soundButtonOn.targetGraphic.color = _offedColor;
            AudioListener.volume = 0;
        }
        _saverLoader.SoundIsActive = isOn;
    }

    public void UpdateLanguage(bool isEnglish)
    {
        if (isEnglish)
        {
            _engButton.targetGraphic.color = _normalColor;
            _deuButton.targetGraphic.color = _offedColor;
        }
        else
        {
            _engButton.targetGraphic.color = _offedColor;
            _deuButton.targetGraphic.color = _normalColor;
        }
        _saverLoader.EnglishIsActive = isEnglish;
    }
    
    private void ActivateLoseWindow()
    {
        DeactivateWindows();
        _loseWindow.SetActive(true);
    }

    private void ActivateWinWindow()
    {
        DeactivateWindows();
    }

    public void ShowBestRecord()
    {
        int bestScore = _saverLoader.LoadBestScore();
        if (_lean.CurrentLanguage == "English")
        {
            _bestScoreText.text =  "Best Score: " + bestScore;
        }
        else
        {
            _bestScoreText.text = "Beste Bewertung: " + bestScore;
        }
    }
    
    private void DeactivateWindows()
    {
        _loseWindow.SetActive(false);
        
    }

    public void UpdateCompleteLevel(int completed = -1)
    {
        if (completed == -1)
        {
            completed = _saverLoader.LevelText;
        }
        if (_lean.CurrentLanguage == "English")
        {
            _completedLevel.text = "Level Completed: " + completed.ToString();
            _endCompletedLevel.text = "Level Completed: " + completed.ToString();
            _pauseCompletedLevel.text = "Level Completed: " + completed.ToString();
        }
        else
        {
            _completedLevel.text = "Level abgeschlossen: " + completed.ToString();
            _endCompletedLevel.text = "Level abgeschlossen: " + completed.ToString();
            _pauseCompletedLevel.text = "Level abgeschlossen: " + completed.ToString();

        }
    }
    
    private void Subscribe()
    {
        _gameState.Lose += ActivateLoseWindow;
        _gameState.Win += ActivateWinWindow;
        _gameState.StartRun += DeactivateWindows;
        _saverLoader.LevelCompleteUpdated += UpdateCompleteLevel;
    }

    private void Describe()
    {
        _gameState.Lose -= ActivateLoseWindow;
        _gameState.Win -= ActivateWinWindow;
        _gameState.StartRun -= DeactivateWindows;
        _saverLoader.LevelCompleteUpdated -= UpdateCompleteLevel;
    }

    private void UpdateOptions()
    {
        UpdateSound(_saverLoader.SoundIsActive);
        UpdateLanguage(_saverLoader.EnglishIsActive);
    }

    private void OnDisable()
    {
        Describe();
    }

    private void Update()
    {
        UpdateCompleteLevel();
        ShowBestRecord();
    }
}
