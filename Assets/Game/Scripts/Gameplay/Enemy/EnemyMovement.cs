using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(MoveScript))]
/* É necessário que os inimigos se movimentem através de uma lista de Vector2 garantindo que não vão atingir nenhum obstáculo ou passar de paredes. A lista será acessada por um outro Script
 * que dita a movimentação.
 */
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] MovementData movementData;//scritableObject que contém a lista de movimentos
    private List<Vector2> movementList;//lista interna de movimentos
    int cont;
    bool enemyCanWalkBool, isMoving;
    private static PlayerScript playerInstance;
    private static GameManager gameManagerInstance;
    private static MoveScript moveScriptInstance;
    void Start()
    {
        movementList = movementData.GetMovementList();//Recebe a lista de movimentos do scritableObject
        gameManagerInstance = GameManager.InstanceManager;
        moveScriptInstance = this.GetComponent<MoveScript>();
        StartCoroutine(EnemyMove());
    }
    private void Update()
    {
        enemyCanWalkBool = gameManagerInstance.moveBool;
    }
    IEnumerator EnemyMove()
    {
        while (true)
        {
            if (enemyCanWalkBool && !isMoving)
            {
                isMoving = true;
                moveScriptInstance.receptMove(movementList[cont]);
                cont++;
                if (cont >= movementList.Count)
                {
                    cont = 0;
                }
                yield return new WaitForSeconds(gameManagerInstance.bps);
                isMoving = false;
            }
            else
            {
                yield return null;
            }
        }
    }
}