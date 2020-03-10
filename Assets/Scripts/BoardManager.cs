using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    //private const float TILE_SIZE = 1.0f;
    //private const float TILE_OFFSET = 0.5f;

    private int TILE_NUMBER_X = 20;
    private int TILE_NUMBER_Z = 20;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // 1. Calcular tiles (dividir tablero en casillas)

    }

    public void GetTile(Vector2 point)
    {

    }

    public Plane GetBasePlane()
    {
        var filter = this.GetComponentInChildren<MeshFilter>();
        Vector3 normal = filter.transform.TransformDirection(filter.mesh.normals[0]);
        return new Plane(normal, transform.position);
    }

    private void SetTiles()
    {

    }

    private void DrawBoard()
    {
        Vector3 xLine = Vector3.right * 20;
        Vector3 zLine = Vector3.forward * 20;

        for (int i = 0; i <= 20; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + xLine);
            for (int j = 0; j <= 20; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + zLine);
            }

        }
    }
}
