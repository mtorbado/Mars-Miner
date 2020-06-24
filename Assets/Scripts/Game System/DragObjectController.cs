using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that allows to drag objetcs with the mouse in the game board
/// </summary>
public class DragObjectController : MonoBehaviour { 
    
    // LAYER MASKS
    const int Board_LayerMask = 1 << 8;
    const int GameElement_LayerMask = 1 << 9;

    Plane basePlane;
    BoardManager boardManager;
    Vector3 previousPosition;

    Ray mouseRay;
    Transform cable;

    private void Awake() {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        basePlane = boardManager.GetBasePlane();
        previousPosition = transform.position;
    }

    private void OnMouseOver() {
        // TODO: show cube outline
    }

    private void OnMouseDrag() {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (basePlane.Raycast(mouseRay, out float distance)) {

            boardManager.ShowTileGrid(true);
            transform.position = mouseRay.GetPoint(distance - distance / 10);

            boardManager.ShowSelectedTile(GetHoverPoint(), IsTileUnderClear());
            //DEBUG_GetPlaneHitPoint();
        }
    }

    private void OnMouseUp() {
        boardManager.ShowTileGrid(false);
        SnapIntoPlaneCell();
    }

    /* ============================================================================================================================================= */

    /// <summary>
    /// Returns a point on the game board upon which the object is hovering
    /// </summary>
    /// <returns>Hover point</returns>
    private Vector2 GetHoverPoint() {
        Vector2 hoverPoint = new Vector2(-1,-1);
        Vector3 toGround = this.transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, toGround, out RaycastHit hit, Mathf.Infinity, Board_LayerMask)) {
            hoverPoint.x = hit.point.x;
            hoverPoint.y = hit.point.z;
        }
        return hoverPoint;
    }

    /// <summary>
    /// Returns if the board tile upon which the object is hovering is clear or not
    /// </summary>
    /// <returns>true if tile is clear, false otherwise</returns>
    private bool IsTileUnderClear() {
        bool clear = true;
        Vector3 toGround = this.transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, toGround, out RaycastHit hit, Mathf.Infinity, GameElement_LayerMask)) {
            clear = false;
        }
        return clear;
    }

    /// <summary>
    /// Snaps the object into the board cell bellow it, if said cell is clear
    /// </summary>
    private void SnapIntoPlaneCell() {

        Tile tile = boardManager.GetTile(GetHoverPoint());

        if (IsTileUnderClear() && tile.X!=-1) {
            Vector3 centerOfTile = boardManager.GetCenterPointOfTile(tile);
            // centerOfTile.y = transform.localScale.y / 2;
            transform.position = centerOfTile;
            previousPosition = transform.position;
        }
        else {
            transform.position = previousPosition;
        }
    }

    /* ============================================================== DEBUG FUNCTIONS ============================================================== */

    private void DEBUG_GetPlaneHitPoint() {
        Vector3 toGround = this.transform.TransformDirection(Vector3.down);
        Debug.DrawRay(this.transform.position, toGround * 50, Color.green);
        Debug.Log("hitting the plane at: (" + GetHoverPoint().x + "," + GetHoverPoint().y + ")");
    }
}

