using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ball>())
        {
            FindObjectOfType<GameState>().EndGame();
        }
    }
}
