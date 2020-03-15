using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    //CONSTANT VALUES
    const int board_layerMask = 1 << 8;

    //PUBLIC REFERENCES   
    Rigidbody rb;
    BoardManager boardManager;
    Plane basePlane;


    private void Start()
    {
        boardManager = (BoardManager) GameObject.Find("Board").GetComponent(typeof(BoardManager));
        basePlane = boardManager.GetBasePlane();
    }

    void OnMouseDown()
    {

    }

    private void OnMouseDrag()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (basePlane.Raycast(mouseRay, out float distance))
        {
            boardManager.ShowTileGrid(true); // mostrar grid
            transform.position = mouseRay.GetPoint(distance - distance / 3); // mover cubo en el aire

            boardManager.ShowSelectedTile(getHoverPoint());

            DEBUG_GetPlaneHitPoint();
        }

    }

    private void OnMouseUp()
    {
        boardManager.ShowTileGrid(false);
        SnapIntoPlaneCell();
    }

    /* =================================================================================================================================== */

    /// <summary>
    /// This class does something.
    /// </summary>
    private Vector2 getHoverPoint()
    {
        Vector2 hoverPoint = new Vector2(-1,-1);
        Vector3 toGround = this.transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, toGround, out RaycastHit hit, Mathf.Infinity, board_layerMask))
        {
            hoverPoint.x = hit.point.x;
            hoverPoint.y = hit.point.z;
        }
        return hoverPoint;
    }

    /// <summary>
    /// This class does something.
    /// </summary>
    private void SnapIntoPlaneCell()
    {
        Vector3 centerOfTile = boardManager.GetCenterPointOfTile(boardManager.GetTile(getHoverPoint()));
        centerOfTile.y = transform.localScale.y / 2;
        transform.position = centerOfTile;
    }

    /* ========================================================= DEBUG FUNCTIONS ========================================================= */


    private void DEBUG_GetPlaneHitPoint()
    {
        Vector3 toGround = this.transform.TransformDirection(Vector3.down);
        Debug.DrawRay(this.transform.position, toGround * 50, Color.green); // pintar línea a suelo (DEBUG)
        Debug.Log("hitting the plane at: (" + getHoverPoint().x + "," + getHoverPoint().y + ")");
    }
}

