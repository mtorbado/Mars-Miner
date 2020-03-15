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

    // Start is called before the first frame update
    void Start()
    {
        basePlane = GetBasePlane();
        gridPlane = GameObject.Find("Grid Plane");
        gridPlane.SetActive(false);
        gridMaterial = gridPlane.GetComponent<Renderer>().material;
        tileNumber = gridMaterial.GetInt("_GridSize");

    }

    /* ========================================================= GET METHODS ========================================================= */

    public Plane GetBasePlane()
    {
        var filter = this.GetComponentInChildren<MeshFilter>();
        Vector3 normal = filter.transform.TransformDirection(filter.mesh.normals[0]);
        return new Plane(normal, transform.position);
    }

    public Tile GetTile(Vector2 point)
    {
        Tile tile = new Tile(-1, -1);
        int tileX = (int)Math.Abs(point.x);
        int tileY = (int)Math.Abs(point.y);

        if (tileX >= 0 && tileX <= tileNumber && tileY >= 0 && tileY <= tileNumber)
        {
            tile.X = tileX;
            tile.Y = tileY;
        }
        return tile;
    }

    public Vector3 GetCenterPointOfTile(Tile tile)
    {
        return new Vector3(tile.X + 0.5f, 0, tile.Y + 0.5f);
    }

    /* ===================================================== GRID UPDATE METHODS ===================================================== */

    public void ShowSelectedTile(Vector2 point)
    {
        Tile selectedTile = GetTile(point);
        if (selectedTile.X != -1 && selectedTile.Y != -1)
        {
            gridMaterial.SetInt("_SelectedCellX", selectedTile.X);
            gridMaterial.SetInt("_SelectedCellY", selectedTile.Y);
            gridMaterial.SetInt("_SelectCell", 1);
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