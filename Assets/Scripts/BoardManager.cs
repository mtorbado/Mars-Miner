using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DrawBoard();
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
