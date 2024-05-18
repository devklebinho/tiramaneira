using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(MoveScript))]
[RequireComponent(typeof(Animator))]
/*� necess�rio um script para o player se movimentar em 4 dire��es e essa movimenta��o ser� liberada ap�s um determinado per�odo de tempo.
 *Tamb�m s�o necess�rios limitadores do espa�o de movimento para que o personagem n�o passe do cen�rio.
 *Futuramente ser� implementado a rea��o do personagem quanto aos NPCs e aos obst�culos no cen�rio.
 */
public class PlayerScript : MonoBehaviour
{
    [Header("Essentials")]
    Rigidbody2D myRigidbody;
    BoxCollider2D myBodyCollider;
    Animator myAnimator;
    [Header("References")]
    [SerializeField] float paddingLeft, paddingRight, paddingTop, paddingBottom, boundariesX, boundariesY, timerMovement;
    public bool ableToWalk, withoutObstacles, isDeadBool;
    [SerializeField] GameObject obstaclesParent;
    [SerializeField] Transform[] obstaclesGameObjects;
    //Constants
    Vector2 moveInput, minBounds, maxBounds;
    float countdownRestart = 2f;
    [SerializeField] Vector3[] obstaclesGameObjectsVectors;
    private static PlayerScript playerInstance;
    private static GameManager gameManagerInstance;
    private static MoveScript moveScriptInstance;
    private static Breath breathScriptInstance;
    private void Awake()
    {
        if (PlayerScript.playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Destroy(PlayerScript.playerInstance);
        }
        gameManagerInstance = GameManager.InstanceManager;
        breathScriptInstance = Breath.breathInstance;
        moveScriptInstance = this.GetComponent<MoveScript>();
    }
    public static PlayerScript InstancePlayer { get { return PlayerScript.playerInstance; } }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        withoutObstacles = true;
        SetUpMoveBoundaries();
        SetUpObstacles();
    }
    void SetUpMoveBoundaries()
    {
        minBounds = new Vector2(-boundariesX, -boundariesY);
        maxBounds = new Vector2(boundariesX, boundariesY);
    }
    void SetUpObstacles()
    {
        obstaclesParent = GameObject.Find("ObstaclesGroup");
        obstaclesGameObjects = new Transform[obstaclesParent.transform.childCount];
        obstaclesGameObjectsVectors = new Vector3[obstaclesParent.transform.childCount];
        for (int i = 0; i < obstaclesParent.transform.childCount; i++)
        {
            obstaclesGameObjects[i] = obstaclesParent.transform.GetChild(i);
            obstaclesGameObjectsVectors[i] = obstaclesGameObjects[i].transform.position;
        }
    }
    void Update()
    {
        ConstantBoudaries();
        DeathState();
        if (!isDeadBool) //Stop Walk
        {            
            ableToWalk = gameManagerInstance.moveBool;
        }
    }
    void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        StartCoroutine(MovePlayer());
    }
    IEnumerator MovePlayer()
    {
        if (moveInput != Vector2.zero)
        {
            var nextMove = (Vector2)transform.position + moveInput;

            foreach (var positionsObstacles in obstaclesGameObjectsVectors)
            {
                if (nextMove == (Vector2)positionsObstacles)
                {
                    withoutObstacles = false;
                    break;
                }
                else
                {
                    withoutObstacles = true;
                }
            }
            if (ableToWalk && withoutObstacles)
            {
                moveScriptInstance.receptMove(moveInput);
                yield return new WaitForSeconds(gameManagerInstance.bps);
                myAnimator.SetTrigger("WalkAnim");
                moveInput = Vector2.zero;
                moveScriptInstance.receptMove(moveInput);
                yield return new WaitForSeconds(1f);
            }
        }
    }   
    private void DeathState()
    {
        if (breathScriptInstance.breathBar.fillAmount <= 0)
        {
            myAnimator.SetBool("AsthmaAnim", true);            
            isDeadBool = true;
            countdownRestart -= Time.deltaTime;            
            if(countdownRestart <= 0f) //Restart Scene
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    private void ConstantBoudaries()
    {
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }
}