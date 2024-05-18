using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(MoveScript))]
/*� necess�rio um script para o player se movimentar em 4 dire��es e essa movimenta��o ser� liberada ap�s um determinado per�odo de tempo.
 *Tamb�m s�o necess�rios limitadores do espa�o de movimento para que o personagem n�o passe do cen�rio.
 *Futuramente ser� implementado a rea��o do personagem quanto aos NPCs e aos obst�culos no cen�rio.
 */
public class PlayerScript : MonoBehaviour
{
    [Header("Essentials")]
    Rigidbody2D myRigidbody;
    BoxCollider2D myBodyCollider;
    [Header("References")]
    [SerializeField] float paddingLeft, paddingRight, paddingTop, paddingBottom, boundariesX, boundariesY, timerMovement;
    public bool ableToWalk, withoutObstacles;
    [SerializeField] GameObject obstaclesParent;
    [SerializeField] Transform[] obstaclesGameObjects;
    //Constants
    Vector2 moveInput, minBounds, maxBounds;
    [SerializeField] Vector3[] obstaclesGameObjectsVectors;
    private static PlayerScript playerInstance;
    private static GameManager gameManagerInstance;
    private static MoveScript moveScriptInstance;
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
        moveScriptInstance = this.GetComponent<MoveScript>();
    }
    public static PlayerScript InstancePlayer { get { return PlayerScript.playerInstance; } }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<BoxCollider2D>();
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
        ableToWalk = gameManagerInstance.moveBool;
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
                } else
                {
                    withoutObstacles = true;
                }
            }
            if (withoutObstacles)
            {
                moveScriptInstance.receptMove(moveInput);
                yield return new WaitForSeconds(gameManagerInstance.bps);
                moveInput = Vector2.zero;
                moveScriptInstance.receptMove(moveInput);
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