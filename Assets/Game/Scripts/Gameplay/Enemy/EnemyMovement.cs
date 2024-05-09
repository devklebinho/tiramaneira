using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] List<Vector2> movementList;
    int cont = 0;
    GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.instanceManager;
    }

    void Start()
    {
        //InvokeRepeating("EnemyMove", 0f, 0.5f);
        StartCoroutine(EnemyMove());
        
    }

    IEnumerator EnemyMove()
    {
        //for (cont = 0; cont < movementList.Count; cont++)
        //{
        //    transform.Translate(movementList[cont].x, movementList[cont].y, 0f);
        //    yield return new WaitForSeconds(2.0f);
        //    Debug.Log("cont");
        //}

        if (cont >= movementList.Count)
        {
            cont = 0;
        }
        transform.Translate(movementList[cont].x, movementList[cont].y, 0f);
        cont++;
        yield return new WaitForSeconds(gameManager.sceneTimer);
    }

}
