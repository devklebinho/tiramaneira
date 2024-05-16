using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.tag == "Player")
            {
                Breath.breathInstance.DecreaseBreath(damage);
                Debug.Log("Lascou o player");
            }
        }
    }
}
