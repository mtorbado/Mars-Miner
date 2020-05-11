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
    float moveSpeed = 1f;

    private void Awake()
    {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        CurrentPosition = transform.position;
    }

    private Tile GetFowardTile()
    {
        Tile t = boardManager.GetTile(transform.position);
        if (transform.rotation.y == 0) 
        {
            return new Tile(t.X + 1, t.Y);
        }
        else if (transform.rotation.y == 1) 
        {
            return new Tile (t.X - 1 , t.Y);
        }
        else if (transform.rotation.y > 0)
        {
            return new Tile (t.X, t.Y - 1);
        }
        else //(transform.rotation.y < 0)
        {
            return new Tile (t.X, t.Y + 1);
        }
    }

    IEnumerator MoveFoward()
    {
        Vector3 target = boardManager.GetCenterPointOfTile(GetFowardTile());
        float i = 0.0f;
        while (Vector3.Distance(transform.position, target) > 0.0f)
        {
            i += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(transform.position, target, i);
            Debug.Log("moving to "+ target);
            yield return null;
        }
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
