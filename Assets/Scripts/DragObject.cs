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
            transform.position = mouseRay.GetPoint(distance - distance/2); // mover cubo en el aire
            Vector3 toGround = this.transform.TransformDirection(Vector3.down);

            Debug.DrawRay(this.transform.position, toGround * 50, Color.green); // pintar línea a suelo (DEBUG)

            if (Physics.Raycast(transform.position, toGround, out RaycastHit hit, Mathf.Infinity, board_layerMask))
            {
                Debug.Log("hitting the plane at " + hit.point.ToString());

            }
        }

    }

    private void OnMouseUp()
    {
        
    }

    private void DoTheCubeSnappingThing()
    {

    }
}

