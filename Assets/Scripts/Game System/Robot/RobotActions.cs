using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Contains the coroutine-based methods for each action that the robot can execute
/// </summary>
public class RobotActions : MonoBehaviour {

    BoardManager boardManager;
    Vector3 currentPosition;
    new Animation animation;
    float moveSpeed = 1f;
    float rotationTime = 1f;
    bool canMove = true;

    private ILevel level;
    private String rockPattern = "(Magnetic)?Rock[1-9]";

    private void Start() {
        level =  gameObject.GetComponent<ILevel>();
    }

    private void Awake() {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        currentPosition = transform.position;
        animation = gameObject.GetComponent<Animation>();
        animation.PlayQueued("RunningRobotAnimation");
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
        animation.Stop();
        animation.PlayQueued("BreakingRobotAnimation");
        GetComponent<ParticleSystem>().Play();
        while(animation.isPlaying) {
            yield return null;
        }
    }

    /* =========================================================== TILE CHECKS ====================================================================== */

    /// <summary>  
    /// Checks if the tile in front is clear
    /// </summary>
    /// <returns> true if tile in front is clear, false otherwise</returns>
    public bool IsFrontTileClear() {
        if (boardManager.isTileClear(GetFowardTile())) {
            return true;
        }
        return false;
    }

    /// <summary>  
    /// Checks if the tile in front has a rock
    /// </summary>
    /// <returns> true if tile in front is a rock, false otherwise</returns>
    public bool IsRockInFront() {
        Regex rg = new Regex(rockPattern);
        UnityEngine.Object frontObject = boardManager.GetTileObject(GetFowardTile());
        if (frontObject != null) {
            if (rg.IsMatch(boardManager.GetTileObject(GetFowardTile()).name)) {
                return true;
            }
        }
        return false;
    }

    /// <summary>  
    /// Checks if the tile in front has an ore
    /// </summary>
    /// <returns> true if tile in front is an ore, false otherwise</returns>
    public bool IsOreInFront() {
        UnityEngine.Object frontObject = boardManager.GetTileObject(GetFowardTile());
        if (frontObject != null) {
            if (boardManager.GetTileObject(GetFowardTile()).name.Equals("Ore")) {
                return true;
            }
        }
        return false;
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
