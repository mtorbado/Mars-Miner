using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    //LAYER MASKS
    const int board_layerMask = 1 << 8;
    const int gameElement_layerMask = 1 << 9;

    //PUBLIC REFERENCES   
    BoardManager boardManager;
    Plane basePlane;
    Vector3 previousPosition;

    private void Awake()
    {
        boardManager = (BoardManager)GameObject.Find("Board").GetComponent(typeof(BoardManager));
        basePlane = boardManager.GetBasePlane();
        previousPosition = transform.position;
    }
    private void Start()
    {
        
    }

    void OnMouseDown()
    {

    }
    private void OnMouseOver()
    {
        
    }

    private void OnMouseDrag()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (basePlane.Raycast(mouseRay, out float distance))
        {

            boardManager.ShowTileGrid(true); // mostrar grid
            transform.position = mouseRay.GetPoint(distance - distance / 10); // mover cubo en el aire
            
            boardManager.ShowSelectedTile(GetHoverPoint(), IsTileClear());

            //DEBUG_GetPlaneHitPoint();
        }

    }

    private void OnMouseUp()
    {
        boardManager.ShowTileGrid(false);
        SnapIntoPlaneCell();
    }

    /* =================================================================================================================================== */

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Vector2 GetHoverPoint()
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
    /// 
    /// </summary>
    /// <returns></returns>
    private bool IsTileClear()
    {
        bool clear = true;
        Vector3 toGround = this.transform.TransformDirection(Vector3.down);
        if (Physics.Raycast(transform.position, toGround, out RaycastHit hit, Mathf.Infinity, gameElement_layerMask))
        {
            clear = false;
        }
        return clear;
    }

    /// <summary>
    /// This class does something.
    /// </summary>
    private void SnapIntoPlaneCell()
    {

        Tile tile = boardManager.GetTile(GetHoverPoint());
        if (IsTileClear() && tile.X!=-1)
        {
            Vector3 centerOfTile = boardManager.GetCenterPointOfTile(tile);
            centerOfTile.y = transform.localScale.y / 2;
            transform.position = centerOfTile;
            previousPosition = transform.position;
        }
        else
        {
            transform.position = previousPosition;
        }
    }

    /* ========================================================= DEBUG FUNCTIONS ========================================================= */


    private void DEBUG_GetPlaneHitPoint()
    {
        Vector3 toGround = this.transform.TransformDirection(Vector3.down);
        Debug.DrawRay(this.transform.position, toGround * 50, Color.green); // pintar línea a suelo (DEBUG)
        Debug.Log("hitting the plane at: (" + GetHoverPoint().x + "," + GetHoverPoint().y + ")");
    }
}

