using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RobotActions : MonoBehaviour
{

    BoardManager boardManager;
    Vector3 CurrentPosition;
    float moveSpeed;

    private void Awake()
    {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        CurrentPosition = transform.position;
    }

    IEnumerator MoveFoward()
    {
        // calculate which is the tile in front of the robot (rotation relative to grid) and get its center point (target)
        // while (Vector3.Distance(transform.position, target) > 0.0f)
        // {
        //     transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed*Time.deltaTime);
        //     yield return null;
        // }
        throw new NotImplementedException();
    }

    IEnumerator MoveBackward()
    {
        throw new NotImplementedException();
    }

    IEnumerator TurnRight()
    {
        throw new NotImplementedException();
    }

    IEnumerator TurnLeft()
    {
        throw new NotImplementedException();
    }

    IEnumerator Scan(Vector2 direction)
    {
        throw new NotImplementedException();
    }
}
