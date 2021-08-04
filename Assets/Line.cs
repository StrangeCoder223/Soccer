using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject[] Bars { get => _bars; private set => _bars = value; }

    [SerializeField] private GameObject[] _bars;
}
