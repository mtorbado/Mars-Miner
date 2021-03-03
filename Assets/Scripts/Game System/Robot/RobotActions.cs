﻿using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Contains the coroutine-based methods for each action that the robot can execute
/// </summary>
public class RobotActions : MonoBehaviour {

    BoardManager boardManager;
    Vector3 CurrentPosition;
    float moveSpeed = 1f;
    float rotationTime = 1f;
    bool canMove = true;

    private ILevel level;

    private void Start() {
        level =  gameObject.GetComponent<ILevel>();
    }

    private void Awake() {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        CurrentPosition = transform.position;
    }

    /// <summary>
    /// Manages robot collisions (with ores and rocks)
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("Ore")) {
            Destroy(collider.gameObject);
            GameEvents.current.PickOreTriggerEnter();
        }
        else {
            canMove = false;
            GameEvents.current.LevelFailed();
        }
    }

    /* =========================================================== COROUTINES ====================================================================== */

    /// <summary>
    /// Moves the robot to the tile in front of it
    /// </summary>
    /// <returns> null </returns>
    public IEnumerator MoveFoward() {
        Vector3 target = boardManager.GetCenterPointOfTile(GetFowardTile());
        float i = 0.0f;
        while (Vector3.Distance(transform.position, target) > 0.0f && canMove) {
            i += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(transform.position, target, i);
            yield return null;
        }
    }

    /// <summary>
    /// Moves the robot to the tile on its back, without rotating
    /// </summary>
    /// <returns> null </returns>
    public IEnumerator MoveBackward() {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Rotates the robot to its right 90 degrees
    /// </summary>
    /// <returns> null </returns>
    public IEnumerator TurnRight() {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 90f;
        float t = 0.0f;
        while ( t  < rotationTime && canMove) {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / rotationTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
        }
    }

    /// <summary>
    /// Rotates the robot to its left 90 degrees
    /// </summary>
    /// <returns> null </returns>
    public IEnumerator TurnLeft() {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation - 90f;
        float t = 0.0f;
        while ( t  < rotationTime && canMove) {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / rotationTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
        }
    }

    /// <summary>
    /// Displays the breaking robot animation
    /// </summary>
    /// <returns> null </returns>
    public IEnumerator BreakAnimation() {
        Animation a = GetComponent<Animation>();
        a.Play();
        GetComponent<ParticleSystem>().Play();
        while(a.isPlaying) {
            yield return null;
        }
    }

    /* ============================================================ PRIVATE FUNCTIONS ============================================================= */

    /// <summary>
    /// Returns the tile in front of the robot
    /// </summary>
    /// <returns> tile in front </returns>
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
