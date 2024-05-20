using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObstacle : MonoBehaviour
{
    public bool isTouchingObstacle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            isTouchingObstacle = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
         if (other.CompareTag("Obstacle"))
        {
            isTouchingObstacle = false;
        }
    }
}
