using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inhaler : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float inhaler;
    PlayerScript PlayerCharacter;
    Breath breath;
    private void Start()
    {
        PlayerCharacter = FindFirstObjectByType<PlayerScript>();
        breath = FindFirstObjectByType<Breath>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        breath.IncreaseBreath(inhaler);
        PlayerCharacter.GetComponent<Animator>().SetTrigger("BombAnim");
        Destroy(gameObject);
    }
}