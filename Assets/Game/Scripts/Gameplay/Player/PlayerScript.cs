using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
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
    public float TimerMovementCharacter;
    //Constants
    Vector2 moveInput, minBounds, maxBounds;
    private static PlayerScript playerInstance;
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
    }
    public static PlayerScript InstancePlayer { get { return PlayerScript.playerInstance; } }
    void Start()
    {        
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<BoxCollider2D>();
        SetUpMoveBoundaries();

        timerMovement = TimerMovementCharacter;

    }
    void SetUpMoveBoundaries()
    {
        minBounds = new Vector2(-boundariesX, -boundariesY);
        maxBounds = new Vector2(boundariesX, boundariesY);
    }
    void Update()
    {
        ConstantBoudaries();        
        if (timerMovement > 0)
        {
            timerMovement -= Time.deltaTime;
        }
    }
    void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (timerMovement <= 0.01f)
        {
            Move();
            timerMovement = TimerMovementCharacter;
        }
    }
    void Move()
    {
        transform.Translate(Mathf.RoundToInt(moveInput.x), Mathf.RoundToInt(moveInput.y), 0f);
    }
    private void ConstantBoudaries()
    {
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }
}
