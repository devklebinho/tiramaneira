using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Vector2> movementList;
    int cont = 0;
    void Start()
    {
        InvokeRepeating("EnemyMove", 0f, 0.5f);
    }

    public void EnemyMove()
    {
        if (cont >= movementList.Count)
        {
            cont = 0;
        }
        transform.Translate(movementList[cont].x, movementList[cont].y, 0f);
        cont++;
    }

}
