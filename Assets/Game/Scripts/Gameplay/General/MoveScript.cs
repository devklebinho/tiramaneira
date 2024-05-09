using UnityEngine;
/* Necessário executar o movimento dos elementos da cena, tanto NPCs quanto o player assim que for comandado.
 */
public class MoveScript : MonoBehaviour
{
    public Vector2 intentDirection;
    public void receptMove(Vector2 direction)
    {
        intentDirection = direction;
    }
    public void Move()
    {
        transform.Translate(intentDirection);
    }
}