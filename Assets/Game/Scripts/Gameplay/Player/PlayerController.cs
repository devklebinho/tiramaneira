using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    private Tilemap groundTilemap;
    [SerializeField]
    private Tilemap collisionTilemap;

    private PlayerMovement controls;

    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        controls = new PlayerMovement();
        boxCollider2D = new BoxCollider2D();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction)
    {
        if (CanMove(direction))
            transform.position += (Vector3)direction;
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;
        else return true;
    }

}