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
    private MusicManager musicManager;
    public Transform checkObstacle;

    void Start()
    {
        musicManager = FindAnyObjectByType<MusicManager>();
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
        /*if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
        }*/
    }
    void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        MovePlayer();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        
    }
    public void MovePlayer()
    {
        if (moveInput != Vector2.zero)
        {
            checkObstacle.position = transform.position + new Vector3(moveInput.x,moveInput.y,0);
            var nextMove = (Vector2)transform.position + moveInput;

            //Debug.Log(musicManager.GetMusicTime() < GameManager.InstanceManager.lastBeatTime + GameManager.InstanceManager.beatInterval - GameManager.InstanceManager.playerMoveTolerance);
            bool canMove = musicManager.GetMusicTime() < GameManager.InstanceManager.lastBeatTime + GameManager.InstanceManager.beatInterval - GameManager.InstanceManager.playerMoveTolerance;

            if (ableToWalk && withoutObstacles && canMove)
            {
                moveScriptInstance.receptMove(moveInput);
                myAnimator.SetTrigger("WalkAnim");
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