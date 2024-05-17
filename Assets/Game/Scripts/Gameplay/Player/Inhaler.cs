using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inhaler : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float inhaler;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Breath.breathInstance.IncreaseBreath(inhaler);
        Destroy(this.gameObject);
    }
}