using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Movement Data", menuName = "ScriptableObjects/Movement Data", order = 1)]
public class MovementData : ScriptableObject
{
    [SerializeField] List<Vector2> movementList = new List<Vector2>();

    public List<Vector2> GetMovementList() => movementList;
}

