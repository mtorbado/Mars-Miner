using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Contains the coroutine-based methods for each action that the robot can execute
/// </summary>
public class RobotActions : MonoBehaviour {

    BoardManager boardManager;
    Vector3 currentPosition;
    new Animation animation;
    AudioManager audioManager;
    float moveSpeed = 1f;
    float rotationTime = 1f;
    bool canMove = true;

    private ILevel level;

    private void Start() {
        level =  gameObject.GetComponent<ILevel>();
    }

    private void Awake() {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        currentPosition = transform.position;
        audioManager = FindObjectOfType<AudioManager>();
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
            audioManager.Play("score");
        }
        else {
            canMove = false;
            GameEvents.current.LevelFailed();
            audioManager.Play("bump");
        }
    }

    /* =========================================================== COROUTINES ====================================================================== */

    /// <summary>
    /// Moves the robot to the tile in front of it
    /// </summary>
    /// <returns> null </returns>
    public IEnumerator MoveFoward() {
        audioManager.Play("robot_motor2");
        Vector3 target = boardManager.GetCenterPointOfTile(GetFowardTile(1));
        float i = 0.0f;
        while (Vector3.Distance(transform.position, target) > 0.0f && canMove) {
            i += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(transform.position, target, i);
            yield return null;
        }
        audioManager.Stop("robot_motor2");
    }

    /// <summary>
    /// Moves the robot to the tile on its back, without rotating
    /// </summary>
    /// <returns> null </returns>
    public IEnumerator MoveBackward() {
        audioManager.Play("robot_motor2");
        Vector3 target = boardManager.GetCenterPointOfTile(GetFowardTile(-1));
        float i = 0.0f;
        while (Vector3.Distance(transform.position, target) > 0.0f && canMove) {
            i += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(transform.position, target, i);
            yield return null;
        }
        audioManager.Stop("robot_motor2");
    }

    /// <summary>
    /// Rotates the robot to its right 90 degrees
    /// </summary>
    /// <returns> null </returns>
    public IEnumerator TurnRight() {
        audioManager.Play("robot_motor3");
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 90f;
        float t = 0.0f;
        while ( t  < rotationTime && canMove) {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / rotationTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
        }
        audioManager.Stop("robot_motor3");
    }

    /// <summary>
    /// Rotates the robot to its left 90 degrees
    /// </summary>
    /// <returns> null </returns>
    public IEnumerator TurnLeft() {
        audioManager.Play("robot_motor3");
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation - 90f;
        float t = 0.0f;
        while ( t  < rotationTime && canMove) {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / rotationTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
        }
        audioManager.Stop("robot_motor3");
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
    /// Checks if the n tile in front has a rock
    /// </summary>
    /// <param name="n"> distance (in tiles) from the robot to the tile to check </param>
    /// <returns> true if tile in front is a rock, false otherwise</returns>
    public bool IsRockInFront(int n) {
        return IsGameElementInFront(n, "Rock");
    }

    /// <summary>  
    /// Checks if the n tile in front has an ore
    /// </summary>
    /// <param name="n"> distance (in tiles) from the robot to the tile to check </param>
    /// <returns> true if tile in front is an ore, false otherwise</returns>
    public bool IsOreInFront(int n) {
        return IsGameElementInFront(n, "Ore");
    }

    /* ============================================================ PRIVATE FUNCTIONS ============================================================= */

    /// <summary>
    /// Returns the n tile in front of the robot
    /// </summary>
    /// <param name="n"> distance (in tiles) from the robot to the returned tile </param>
    /// <returns> tile in front </returns>
    private Tile GetFowardTile(int n) {
        Tile t = boardManager.GetTile(transform.position);
        if (transform.forward == new Vector3(1,0,0)) {
            return new Tile(t.X + n, t.Y);
        }
        else if (transform.forward == new Vector3(-1,0,0)) {
            return new Tile (t.X - n , t.Y);
        }
        else if (transform.forward == new Vector3(0,0,1)) {
            return new Tile (t.X, t.Y + n);
        }
        else {
            return new Tile (t.X, t.Y - n);
        }
    }

    /// <summary>  
    /// Checks if the tile in front of the robot has a game element with the given tag
    /// </summary>
    /// <param name="tag"> tag of the game element</param>
    /// <returns> true if there's a game object in front of the robot that mathes the tag, false otherwise</returns>
    private bool IsGameElementInFront(int n, String tag) {
        GameObject frontObject = boardManager.GetTileObject(GetFowardTile(n));
        if (frontObject != null) {
            if (boardManager.GetTileObject(GetFowardTile(n)).CompareTag(tag)) {
                return true;
            }
        }
        return false;
    }
}