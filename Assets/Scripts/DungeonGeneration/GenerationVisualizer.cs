using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerationVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTiles, wallTiles;

    [SerializeField]
    private TileBase candidateTile, boundary;

    public void PaintFloor(IEnumerable<Vector2Int> floorCoords)
    {
        PaintTiles(floorCoords, floorTiles, candidateTile);
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTiles, boundary, position);
    }

    private void PaintTiles(IEnumerable<Vector2Int> floorPos, Tilemap tilemap, TileBase candidate)
    {
        foreach(var pos in floorPos)
        {
            PaintSingleTile(tilemap, candidate, pos);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int pos)
    {
        var tilePos = tilemap.WorldToCell((Vector3Int)pos);
        tilemap.SetTile(tilePos, tile);
    }

    public void Clear()
    {
        floorTiles.ClearAllTiles();
        wallTiles.ClearAllTiles();
    }
}
