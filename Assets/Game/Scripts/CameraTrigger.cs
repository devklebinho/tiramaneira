using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public float newPlayerPositionX, newPlayerpositionY, newPlayerpositionZ;
   // public Camera cam;
    private PlayerScript player;
    // Start is called before the first frame update
    void Start()
    {
        //cam = Camera.main;
        player = FindFirstObjectByType<PlayerScript>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //cam.transform.position = newCameraPosition;
            player.transform.position = new Vector3(newPlayerPositionX, newPlayerpositionY, newPlayerpositionZ);
        }
    }
}
