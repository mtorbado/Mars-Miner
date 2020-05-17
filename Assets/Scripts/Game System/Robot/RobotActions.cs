using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RobotActions : MonoBehaviour {

    BoardManager boardManager;
    Vector3 CurrentPosition;
    float moveSpeed = 1f;
    float rotationTime = 1f;

    private void Awake() {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        CurrentPosition = transform.position;
    }

    /* =========================================================== COROUTINES ====================================================================== */

    public IEnumerator MoveFoward() {
        Vector3 target = boardManager.GetCenterPointOfTile(GetFowardTile());
        float i = 0.0f;
        while (Vector3.Distance(transform.position, target) > 0.0f) {
            i += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(transform.position, target, i);
            // Debug.Log("moving to "+ target);
            yield return null;
        }
    }

    public IEnumerator MoveBackward() {
        throw new NotImplementedException();
    }

    public IEnumerator TurnRight() {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 90f;
        float t = 0.0f;
        while ( t  < rotationTime ) {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / rotationTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
        }
    }

    public IEnumerator TurnLeft() {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation - 90f;
        float t = 0.0f;
        while ( t  < rotationTime ) {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / rotationTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
        }
    }

    public void Stop() {
        // TODO: implement
    }

    /* ============================================================ PRIVATE FUNCTIONS ============================================================= */

    private Tile GetFowardTile() {

        Tile t = boardManager.GetTile(transform.position);

        if (transform.rotation.y == 0) {
            return new Tile(t.X + 1, t.Y);
        }

        else if (transform.rotation.y == 1) {
            return new Tile (t.X - 1 , t.Y);
        }

        else if (transform.rotation.y > 0) {
            return new Tile (t.X, t.Y - 1);
        }

        else { //(transform.rotation.y < 0)
            return new Tile (t.X, t.Y + 1);
        }
    }
}
