using System.Collections;
using System.Collections.Generic;
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
    public List<MoveScript> moveScripts;
    public float bps, inputDelay;
    public bool moveBool;
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
    public static GameManager InstanceManager { get { return GameManager.managerInstance; } }
    void Start()
    {
        currentscene = SceneManager.GetActiveScene().buildIndex;
        SetTimerMovement();
        this.PlayerCharacter = PlayerScript.InstancePlayer;       
        StartCoroutine(DanceRoutine());
    }
    IEnumerator DanceRoutine()
    {
        yield return new WaitForSeconds(bps);
        moveBool = true;
        yield return new WaitForSeconds(inputDelay);
        foreach (MoveScript move in moveScripts)
        {
            move.Move();
            moveBool = false;
        }
        StartCoroutine(DanceRoutine());
    }
    private void SetTimerMovement()
    {
        switch (currentscene)
        {
            case 1:
                bps = 1f;
                inputDelay = 1f;
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