using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "New Movement Data", menuName = "ScriptableObjects/Movement Data", order = 1)]
public class MovementData : ScriptableObject
{
    [SerializeField] List<Vector2> movementList = new List<Vector2>();
    public List<Vector2> GetMovementList() { return movementList; }

    public void AddMovement(Vector2 movement)
    {
        movementList.Add(movement);
    }

    public void ClearMovement()
    {
        movementList.Clear();
    }

    public void AddUp()
    {
        AddMovement(Vector2.up);
    }

    public void AddDown()
    {
        AddMovement(Vector2.down);
    }

    public void AddLeft()
    {
        AddMovement(Vector2.left);
    }

    public void AddRight()
    {
        AddMovement(Vector2.right);
    }

    public void ReverseList()
    {
        List<Vector2> reversedList = new List<Vector2>(movementList);
        reversedList.Reverse();
        movementList = reversedList;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(MovementData))]
public class MovementDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MovementData movementData = (MovementData)target;
        // Bot�o para adicionar um Vector2 (0, 1) - Cima
        if (GUILayout.Button("Add Up"))
        {
            movementData.AddMovement(Vector2.up);
        }
        // Bot�o para adicionar um Vector2 (0, -1) - Baixo
        if (GUILayout.Button("Add Down"))
        {
            movementData.AddMovement(Vector2.down);
        }
        // Bot�o para adicionar um Vector2 (-1, 0) - Esquerda
        if (GUILayout.Button("Add Left"))
        {
            movementData.AddMovement(Vector2.left);
        }
        // Bot�o para adicionar um Vector2 (1, 0) - Direita
        if (GUILayout.Button("Add Right"))
        {
            movementData.AddMovement(Vector2.right);
        }
        // Bot�o para Limpar a lista criada
        if (GUILayout.Button("Clean List"))
        {
            movementData.ClearMovement();
        }
        // Bot�o para Inverter a lista criada
        if (GUILayout.Button("Reverse List"))
        {
            movementData.ReverseList();
        }
    }
}
#endif
