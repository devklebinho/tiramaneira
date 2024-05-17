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
    [Range(0f, 1f)]
    public float breathPoints;//NEW: Pontos de f�lego para decrementar a cada beat da m�sica
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
        Breath.breathInstance.DecreaseBreath(breathPoints);//NEW: decrementa ponto de f�lego conforme o beat da m�sica.
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
                bps = 0.5f;
                inputDelay = 0.5f;
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
