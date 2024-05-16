using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inhaler : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float inhaler;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Breath.breathInstance.IncreaseBreath(inhaler);
            Debug.Log("Curou o player");
            Destroy(this.gameObject);
        }
    }
}
