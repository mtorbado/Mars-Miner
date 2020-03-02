using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{

    Rigidbody rb;
    private Ray mouseRay, cubeRay;
    public Plane basePlane;

    private void Start()
    {
        Vector3 normal;
        rb = GetComponent<Rigidbody>();
        var filter = GameObject.Find("Plane").GetComponent<MeshFilter>();

        normal = filter.transform.TransformDirection(filter.mesh.normals[0]);
        basePlane = new Plane(normal, transform.position);
    }

    void OnMouseDown()
    {

    }

    private void OnMouseDrag()
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (basePlane.Raycast(mouseRay, out float distance))
        {
            // get the hit point:
            transform.position = mouseRay.GetPoint(distance);
        }
    }

    private void OnMouseUp()
    {
        

    }
}

