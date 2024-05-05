using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;      
public class PlayerActions : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementInput;

    //Action maps
    public static string PlatformerMap = "Platform";
    public static string TopDownMap = "TopDown";

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }


    //Action maps switch
    public void SwitchActionMap(string mapName)
    {
        playerInput.SwitchCurrentActionMap(mapName);
    }

    public void PlatformActionMap(InputAction.CallbackContext action)
    {
        if (action.performed)
            Debug.Log("Platform Action Map Activated");
    }

    public void TopDownActionMap(InputAction.CallbackContext action)
    {
        if (action.performed)
            Debug.Log("Top Down Action Map Activated");
    }

    //Platform movements
    public void ShortJump(InputAction.CallbackContext action)
    {
        if (action.performed)
            Debug.Log("Short Jump");
    }

    public void LongJump(InputAction.CallbackContext action)
    {
        if (action.performed)
            Debug.Log("Long Jump");
    }

    public void MoveOnPlatform(InputAction.CallbackContext action)
    {
        if (action.performed)
        {
            Debug.Log($"x: {action.ReadValue<Vector2>().x}, y: {action.ReadValue<Vector2>().y}");
            Debug.Log("Platform movement");
        }
    }

    //Top Down movements
    public void MoveTopDown(InputAction.CallbackContext action)
    {
        if (action.performed)
        {
            movementInput = action.ReadValue<Vector2>();
            Debug.Log($"x: {action.ReadValue<Vector2>().x}, y: {action.ReadValue<Vector2>().y}");
            Debug.Log("Top Down movement");
        }
    }
}
