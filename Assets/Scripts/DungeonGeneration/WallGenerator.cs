using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator 
{
    public static void CreateWalls(HashSet<Vector2Int> floorCoords, GenerationVisualizer visualizer)
    {
        var wallPositions = FindRelativeWallFace(floorCoords, Direction2D.directionEnums);
        foreach (var position in wallPositions)
        {
            visualizer.PaintSingleBasicWall(position);
        }
    }

    private static HashSet<Vector2Int> FindRelativeWallFace(HashSet<Vector2Int> floorCoords, List<Vector2Int> directionEnums)
    {
        HashSet<Vector2Int> wallCoords = new HashSet<Vector2Int>();
        foreach (var position in floorCoords)
        {
            foreach (var dir in directionEnums)
            {
                var adjacentPos = position + dir;
                if(!floorCoords.Contains(adjacentPos))
                {
                    wallCoords.Add(adjacentPos);
                }
            }
        }
        return wallCoords;
    }
}
