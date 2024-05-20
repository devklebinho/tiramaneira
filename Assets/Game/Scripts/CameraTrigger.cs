using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public float newPlayerPositionX, newPlayerpositionY, newPlayerpositionZ;
    private PlayerScript player;
    void Start()
    {
        player = FindFirstObjectByType<PlayerScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.transform.position = new Vector3(newPlayerPositionX, newPlayerpositionY, newPlayerpositionZ);
        }
    }
}
