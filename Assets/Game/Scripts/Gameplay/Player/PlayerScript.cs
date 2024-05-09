using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(MoveScript))]
/*É necessário um script para o player se movimentar em 4 direções e essa movimentação será liberada após um determinado período de tempo.
 *Também são necessários limitadores do espaço de movimento para que o personagem não passe do cenário.
 *Futuramente será implementado a reação do personagem quanto aos NPCs e aos obstáculos no cenário.
 */
public class PlayerScript : MonoBehaviour
{
    [Header("Essentials")]
    Rigidbody2D myRigidbody;
    BoxCollider2D myBodyCollider;
    [Header("References")]
    [SerializeField] float paddingLeft, paddingRight, paddingTop, paddingBottom, boundariesX, boundariesY, timerMovement;
    public bool ableToWalk;
    //Constants
    Vector2 moveInput, minBounds, maxBounds;
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
    }    public static PlayerScript InstancePlayer { get { return PlayerScript.playerInstance; } }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<BoxCollider2D>();
        SetUpMoveBoundaries();
    }
    void SetUpMoveBoundaries()
    {
        minBounds = new Vector2(-boundariesX, -boundariesY);
        maxBounds = new Vector2(boundariesX, boundariesY);
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
        if (moveInput != Vector2.zero && ableToWalk)
        {
            moveScriptInstance.receptMove(moveInput);
            yield return new WaitForSeconds(gameManagerInstance.bps);
            moveInput = Vector2.zero;
            moveScriptInstance.receptMove(moveInput);
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
