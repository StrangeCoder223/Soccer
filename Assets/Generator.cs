using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private Line[] _lines;
    [SerializeField] private int _maxBars;
   

    public void GenerateLevel(int currentLevel)
    {
        _maxBars = Mathf.RoundToInt(currentLevel/2);
        int actualLine = Random.Range(0, 3);
        for (int i = 0; i < _maxBars; i++)
        {
            int actualBar = Random.Range(0,_lines[actualLine].Bars.Length-1);
            _lines[actualLine].Bars[actualBar].SetActive(true);
        }
    }

}
