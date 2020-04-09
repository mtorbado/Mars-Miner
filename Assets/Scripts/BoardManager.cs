using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    private int tileNumber;
    private double tileSize;

    Rigidbody rb;
    Plane basePlane;
    GameObject gridPlane;
    Material gridMaterial;


    void Start()
    {
        basePlane = GetBasePlane();
        gridPlane = GameObject.Find("Grid Plane");
        gridPlane.SetActive(false);
        gridMaterial = gridPlane.GetComponent<Renderer>().material;
        tileNumber = gridMaterial.GetInt("_GridSize");

    }

    /* ========================================================= GET METHODS ========================================================= */

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Plane GetBasePlane()
    {
        var filter = this.GetComponentInChildren<MeshFilter>();
        Vector3 normal = filter.transform.TransformDirection(filter.mesh.normals[0]);
        return new Plane(normal, transform.position);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public Tile GetTile(Vector2 point)
    {
        Tile tile = new Tile(-1, -1);
        if (point.x >= 0 && point.x <= tileNumber && point.y >= 0 && point.y <= tileNumber)
        {
            tile.X = (int)Math.Abs(point.x);
            tile.Y = (int)Math.Abs(point.y);
        }
        return tile;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tile"></param>
    /// <returns></returns>
    public Vector3 GetCenterPointOfTile(Tile tile)
    {
        return new Vector3(tile.X + 0.5f, 0, tile.Y + 0.5f);
    }

    /* ===================================================== GRID UPDATE METHODS ===================================================== */

    /// <summary>
    /// 
    /// </summary>
    /// <param name="point"></param>
    /// <param name="isClear"></param>
    public void ShowSelectedTile(Vector2 point, bool isClear)
    {
        Tile selectedTile = GetTile(point);
        if (selectedTile.X != -1 && selectedTile.Y != -1)
        {
            if (isClear)
            {
                gridMaterial.SetColor("_SelectedColor", new Color(0.7f,1,0.36f));
                gridMaterial.SetColor("_LineColor", new Color(0.8f,1,0.16f));
            }
            else
            {
                gridMaterial.SetColor("_SelectedColor", new Color(1,0.4f,0.2f));
                gridMaterial.SetColor("_LineColor", new Color(1,0.4f,0.2f));
            }
            gridMaterial.SetInt("_SelectedCellX", selectedTile.X);
            gridMaterial.SetInt("_SelectedCellY", selectedTile.Y);
            gridMaterial.SetInt("_SelectCell", 1);
        }
        else
        {
            gridMaterial.SetInt("_SelectCell", 0);
        }
    }

    public void ShowTileGrid(bool value)
    {
        gridPlane.SetActive(value);
    }
}


/* ========================================================= AUXILIAR CLASS ========================================================== */


public class Tile
{
    public int X { get; set; }
    public int Y { get; set; }

    public Tile(int x, int y) => (X, Y) = (x, y);
}