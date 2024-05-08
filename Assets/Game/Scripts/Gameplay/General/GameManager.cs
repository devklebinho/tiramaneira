using UnityEngine;
using UnityEngine.SceneManagement;
/*Por enquanto, no GameManager, est� sendo necess�rio uma defini��o de "timer" por cena, que ser� de acordo com a batida da m�sica
*para o personagem e os NPCs se movimentarem.
 */
public class GameManager : MonoBehaviour
{
    int currentscene;
    PlayerScript PlayerCharacter;
    private static GameManager managerInstance;
    private void Awake()
    {
        if (GameManager.managerInstance == null)
        {
            managerInstance = this;
        }
        else
        {
            Destroy(GameManager.managerInstance);
        }
    }
    public static GameManager instanceManager { get { return GameManager.managerInstance; } }
    void Start()
    {
        currentscene = SceneManager.GetActiveScene().buildIndex;
        this.PlayerCharacter = PlayerScript.InstancePlayer;
        SetTimerMovement();
    }
    private void SetTimerMovement()
    {
        switch (currentscene)
        {
            case 1:
                PlayerCharacter.TimerMovementCharacter = 3f;
                //Setar o mesmo tempo nos inimigos aqui
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}
