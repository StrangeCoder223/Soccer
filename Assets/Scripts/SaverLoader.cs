using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaverLoader : MonoBehaviour
{
    public delegate void OnSaverHandler(int currentLevel);
    public event OnSaverHandler LevelCompleteUpdated;

    private const string LEVELTEXT = "LevelText";
    private static SaverLoader _saverLoader;
    private bool _soundIsActive;
    private bool _englishIsActive;
    
    public bool SoundIsActive
    {
        get
        {
            if (!PlayerPrefs.HasKey("SoundIsActive"))
            {
                PlayerPrefs.SetInt("SoundIsActive",1);
                _soundIsActive = true;
            }
            _soundIsActive = Convert.ToBoolean(PlayerPrefs.GetInt("SoundIsActive"));
            return _soundIsActive;
        }
        set
        {
            _soundIsActive = value;
            if (_soundIsActive)
            {
                AudioListener.volume = 1;
            }
            else
            {
                AudioListener.volume = 0;
            }
            PlayerPrefs.SetInt("SoundIsActive",Convert.ToInt32(_soundIsActive));
        }
    }
    public bool EnglishIsActive
    {
        get
        {
            if (!PlayerPrefs.HasKey("EnglishIsActive"))
            {
                PlayerPrefs.SetInt("EnglishIsActive", 1);
                _englishIsActive = true;
            }
            _englishIsActive = Convert.ToBoolean(PlayerPrefs.GetInt("EnglishIsActive"));
            return _englishIsActive;
        }
        set
        {
            _englishIsActive = value;
            PlayerPrefs.SetInt("EnglishIsActive", Convert.ToInt32(_englishIsActive));
        }
    }

    public int LevelText
    {
        get
        {
            if (!PlayerPrefs.HasKey(LEVELTEXT))
            {
                PlayerPrefs.SetInt(LEVELTEXT, 0);
            }
            
            return PlayerPrefs.GetInt(LEVELTEXT);
        }
        set
        {
            PlayerPrefs.SetInt(LEVELTEXT, value);
            int currentLevel = PlayerPrefs.GetInt(LEVELTEXT);
            LevelCompleteUpdated?.Invoke(currentLevel);
            if (currentLevel > LoadBestScore())
            {
                SaveBestScore(currentLevel);
            }
        }
    }

    private void Awake()
    {
        if (_saverLoader == null)
        {
            _saverLoader = this;
        }
    }

    public static SaverLoader GetInstance()
    {
        return _saverLoader;
    }
    
    public void SaveBestScore(int index)
    {
        PlayerPrefs.SetInt("BestScore", index);
    }
    public int LoadBestScore()
    { 
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }
        return PlayerPrefs.GetInt("BestScore");
    }

    /*public void SetFirstLaunch(bool isFirst)
    {
        PlayerPrefs.SetInt("isFirstLaunch",Convert.ToInt32(isFirst));
    }
    public bool IsFirstLaunch()
    {
        if (!PlayerPrefs.HasKey("isFirstLaunch"))
        {
            PlayerPrefs.SetInt("isFirstLaunch",1);
        }
        return Convert.ToBoolean(PlayerPrefs.GetInt("isFirstLaunch"));
    }
    */
    private void OnApplicationQuit()
    {
        LevelText = 0;
    }
}
