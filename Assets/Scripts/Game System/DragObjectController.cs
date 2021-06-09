using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectController : MonoBehaviour { 
    
    // LAYER MASKS
    const int Board_LayerMask = 1 << 8;
    const int GameElement_LayerMask = 1 << 9;

    private bool canDrag;

    Plane basePlane;
    BoardManager boardManager;
    Vector3 previousPosition;

    private void Awake() {
        canDrag = true;
        GameEvents.current.onDisableAllForTutorial += DisableDrag;
        GameEvents.current.onEnableAllAfterTutorial += EnableDrag;

        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        basePlane = boardManager.GetBasePlane();
        previousPosition = transform.position;
    }

    private void OnMouseOver() {
        // TODO: show cube outline
    }

    private void OnMouseDrag() {
        if (canDrag) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (basePlane.Raycast(mouseRay, out float distance)) {
                boardManager.ShowTileGrid(true);
                transform.position = mouseRay.GetPoint(distance - distance / 10);
                boardManager.ShowSelectedTile(GetHoverPoint());
                //DEBUG_GetPlaneHitPoint();
            }
        }
    }

    private void OnMouseUp() {
        boardManager.ShowTileGrid(false);
        SnapIntoPlaneCell();
    }

    /* =============================================================== AUXILIAR METHODS =============================================================== */

    private Vector2 GetHoverPoint() {
        Vector2 hoverPoint = new Vector2(-1,-1);
        Vector3 toGround = this.transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, toGround, out RaycastHit hit, Mathf.Infinity, Board_LayerMask)) {
            hoverPoint.x = hit.point.x;
            hoverPoint.y = hit.point.z;
        }
        return hoverPoint;
    }

    private void SnapIntoPlaneCell() {
        Tile tile = boardManager.GetTile(GetHoverPoint());

        if (boardManager.isTileClear(tile) && tile.X!=-1) {
            Vector3 centerOfTile = boardManager.GetCenterPointOfTile(tile);
            // centerOfTile.y = transform.localScale.y / 2;
            transform.position = centerOfTile;
            previousPosition = transform.position;
        }
        else {
            transform.position = previousPosition;
        }
    }

    /* ============================================================== TUTORIAL LEVEL ============================================================== */

    private void DisableDrag() {
        canDrag = false;
    }

    private void EnableDrag() {
        canDrag = true;
    }

    /* ============================================================== DEBUG FUNCTIONS ============================================================== */

    private void DEBUG_GetPlaneHitPoint() {
        Vector3 toGround = this.transform.TransformDirection(Vector3.down);
        Debug.DrawRay(this.transform.position, toGround * 50, Color.green);
        Debug.Log("hitting the plane at: (" + GetHoverPoint().x + "," + GetHoverPoint().y + ")");
    }
}

