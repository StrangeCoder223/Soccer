using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public delegate void OnBasketHandler(int count);
    public event OnBasketHandler BallScored;
    private int _currentBalls = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ball ball))
        {
            Destroy(ball.gameObject);
            _currentBalls++;
            BallScored?.Invoke(_currentBalls);
        }
    }
}
