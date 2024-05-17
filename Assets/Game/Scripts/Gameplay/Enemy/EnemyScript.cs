using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(MoveScript))]
/* � necess�rio que os inimigos se movimentem atrav�s de uma lista de Vector2 garantindo que n�o v�o atingir nenhum obst�culo ou passar de paredes. A lista ser� acessada por um outro Script
 * que dita a movimenta��o.
 */
public class EnemyScript : MonoBehaviour
{
    [SerializeField] MovementData movementData;//scritableObject que cont�m a lista de movimentos
    private List<Vector2> movementList;//lista interna de movimentos
    int cont;
    bool enemyCanWalkBool, isMoving;
    private static PlayerScript playerInstance;
    private static GameManager gameManagerInstance;
    private static MoveScript moveScriptInstance;
    [Range(0, 1)]
    [SerializeField] float damage;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Breath.breathInstance.DecreaseBreath(damage);
    }
}