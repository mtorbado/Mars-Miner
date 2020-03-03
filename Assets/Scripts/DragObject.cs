using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{

    Rigidbody rb;
    public Plane basePlane;

    private void Start()
    {
        basePlane = GetBasePlane();
    }

    void OnMouseDown()
    {

    }

    private void OnMouseDrag()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (basePlane.Raycast(mouseRay, out float distance))
        {
            Vector3 toGround = this.transform.TransformDirection(Vector3.down);
            Debug.DrawRay(this.transform.position, toGround * 50, Color.green);

            transform.position = mouseRay.GetPoint(distance);
        }

    }

    private void OnMouseUp()
    {
        

    }
    
    private Plane GetBasePlane()
    {
        rb = GetComponent<Rigidbody>();
        var filter = GameObject.Find("Board").GetComponentInChildren<MeshFilter>();
        Vector3 normal = filter.transform.TransformDirection(filter.mesh.normals[0]);
        return new Plane(normal, transform.position);
    }
}

