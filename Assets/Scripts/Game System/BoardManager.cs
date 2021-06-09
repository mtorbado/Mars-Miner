using System;
using UnityEngine;

/// <summary>
/// Contains multiple utility functions to work with the game booard
/// </summary>
public class BoardManager : MonoBehaviour {

    // LAYER MASKS
    const int GameElement_LayerMask = 1 << 9;

    private int tileNumber;
    private double tileSize;

    Plane basePlane;
    GameObject gridPlane;
    Material gridMaterial;

    void Awake() {
        basePlane = GetBasePlane();
        gridPlane = GameObject.Find("Grid Plane");
        gridPlane.SetActive(false);
        gridMaterial = gridPlane.GetComponent<Renderer>().material;
        tileNumber = gridMaterial.GetInt("_GridSize");
    }

    /* =============================================================== GET METHODS =============================================================== */

    /// <summary>
    /// Returns the board game plane to perform other calculations
    /// </summary>
    /// <returns> board game plane </returns>
    public Plane GetBasePlane() {
        var filter = this.GetComponentInChildren<MeshFilter>();
        Vector3 normal = filter.transform.TransformDirection(filter.mesh.normals[0]);
        return new Plane(normal, transform.position);
    }

    /// <summary>
    /// Returns a game board tile from a point (X,Y) from game world coordinates
    /// </summary>
    /// <param name="point"> 2D point (X,Y) from game world coordinates </param>
    /// <returns></returns>
    public Tile GetTile(Vector2 point) {

        Tile tile = new Tile(-1, -1); //TODO: manage positions outside the board in a different way

        if (point.x >= 0 && point.x <= tileNumber && point.y >= 0 && point.y <= tileNumber) {
            tile.X = (int)Math.Abs(point.x);
            tile.Y = (int)Math.Abs(point.y);
        }
        return tile;
    }

    /// <summary>
    /// Returns a game board tile from a point (X,Y,Z) from game world coordinates
    /// </summary>
    /// <param name="point"> 3D point (X,Y,Z) from game world coordinates </param>
    /// <returns></returns>
    public Tile GetTile(Vector3 point) {
        return GetTile(new Vector2(point.x, point.z));
    }

    /// <summary>
    /// Returns the center point of the given tile
    /// </summary>
    /// <param name="tile"> game board tile </param>
    /// <returns> center pont of the tile </returns>
    public Vector3 GetCenterPointOfTile(Tile tile) {
        return new Vector3(tile.X + 0.5f, 0.5f, tile.Y + 0.5f);
    }

    /// <summary>
    /// Returns the center point of the given tile coordinates
    /// </summary>
    /// <param name="x"> x coordinate of game board tile</param>
    /// <param name="y"> y coordinate of game board tile</param>
    /// <returns> center pont of the tile </returns>
    public Vector3 GetCenterPointOfTile(int x, int y) {
        return new Vector3(x + 0.5f, 0.5f, y + 0.5f);
    }

    /// <summary>
    /// Returns the game element object sitting in a given tile
    /// </summary>
    /// <param name="tile"> tile where the wanted game element is</param>
    /// <returns>the game element object if there's any, null otherwise</returns>
    public GameObject GetTileObject(Tile tile) {
        Vector3 up = this.transform.TransformDirection(Vector3.up);
        Vector3 tileCenter = GetCenterPointOfTile(tile);
        Vector3 rayOrigin = new Vector3(tileCenter.x, tileCenter.y -1f, tileCenter.z);
        if (Physics.Raycast(rayOrigin, up, out RaycastHit hit, 1f, GameElement_LayerMask) && hit.collider != null) {
            return hit.collider.gameObject;
        }
        return null;
    }

    /* ================================================================== OTHER ================================================================== */

    /// <summary>
    /// Checks if there is a game element object in the given tile
    /// </summary>
    /// <param name="tile"> tile to check</param>
    /// <returns> true if the tile is clear, false otherwise</returns>
    public bool isTileClear(Tile tile) {
        Vector3 up = this.transform.TransformDirection(Vector3.up);
        Vector3 tileCenter = GetCenterPointOfTile(tile);
        Vector3 rayOrigin = new Vector3(tileCenter.x, tileCenter.y -1f, tileCenter.z);
        if (Physics.Raycast(rayOrigin, up, out RaycastHit hit, 1f, GameElement_LayerMask)) {
            return false;
        }
        return true;
    }

    /* =========================================================== GRID UPDATE METHODS =========================================================== */

    /// <summary>
    /// Updates the board tile grid color to signal if we are pointing to a clear tile or a filled one
    /// </summary>
    /// <param name="point"></param>
    /// <param name="isClear"></param>
    public void ShowSelectedTile(Vector2 point) {

        Tile selectedTile = GetTile(point);

        if (selectedTile.X != -1 && selectedTile.Y != -1) {
            if (isTileClear(selectedTile)) {
                gridMaterial.SetColor("_SelectedColor", new Color(0.7f,1,0.36f));
                gridMaterial.SetColor("_LineColor", new Color(0.8f,1,0.16f));
            }
            else {
                gridMaterial.SetColor("_SelectedColor", new Color(1,0.4f,0.2f));
                gridMaterial.SetColor("_LineColor", new Color(1,0.4f,0.2f));
            }
            gridMaterial.SetInt("_SelectedCellX", selectedTile.X);
            gridMaterial.SetInt("_SelectedCellY", selectedTile.Y);
            gridMaterial.SetInt("_SelectCell", 1);
            // DEBUG_PrintTile(selectedTile);
        }
        else {
            gridMaterial.SetInt("_SelectCell", 0);
        }
    }

    /// <summary>
    /// Shows or hides the board tile grid
    /// </summary>
    /// <param name="value"> tile grid visivility </param>
    public void ShowTileGrid(bool value) {
        gridPlane.SetActive(value);
    }

    /* ============================================================= DEBUG FUNCTIONS ============================================================= */
    
    private void DEBUG_PrintTile(Tile tile) { 
        Debug.Log("hitting the tile: (" + tile.X + "," + tile.Y + ")");
    }
}