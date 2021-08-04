using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Generator))]
public class Game : MonoBehaviour
{
    
    [SerializeField] private Basket _basket;

    private Generator _generator;
    private SaverLoader _saverLoader;
    private GameState _gameState;
    void Start()
    {
        _saverLoader = SaverLoader.GetInstance();
        _gameState = FindObjectOfType<GameState>();
        _generator = GetComponent<Generator>();
        _generator.GenerateLevel(_saverLoader.LevelText);
        _basket.BallScored += CheckFinish;
    }

    private void CheckFinish(int count)
    {
        if (count >= 3)
        {
            _saverLoader.LevelText++;
            _gameState.WinGame();
        }
    }

    

}
