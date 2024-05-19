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
    private MoveScript moveScriptInstance;
    private Breath breath;

    void Start()
    {
        breath = FindFirstObjectByType<Breath>();
        moveScriptInstance = GetComponent<MoveScript>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        withoutObstacles = true;
        SetUpMoveBoundaries();
        SetUpObstacles();
        ableToWalk = true;
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
                yield return new WaitForSeconds(GameManager.InstanceManager.inputErrorTime);
                myAnimator.SetTrigger("WalkAnim");
                moveInput = Vector2.zero;
                moveScriptInstance.receptMove(moveInput);
                yield return new WaitForSeconds(1f);
            }
        }
    }
    public void DeathState()
    {
        ableToWalk = false;
        myAnimator.SetBool("AsthmaAnim", true);
        isDeadBool = true;
        StartCoroutine(RestarScene());
    }

    IEnumerator RestarScene()
    {
        yield return new WaitForSeconds(countdownRestart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void ConstantBoudaries()
    {
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            breath.DecreaseBreath(collision.GetComponent<EnemyScript>().damage);
        }
    }
}