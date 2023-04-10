using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralDungeonGenerationAlgorithms
{
   
    public static HashSet<Vector2Int> RandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> res = new HashSet<Vector2Int>();

        res.Add(startPosition);

        var lastPosition = startPosition;
        for (int i = 0; i < walkLength; i++)
        {
            var nextPosition = lastPosition + Direction2D.GetRandomDirection();
            res.Add(nextPosition);
            lastPosition = nextPosition;
        }
        return res;
    }

    public static List<Vector2Int> RWCorridor(Vector2Int initialPosition, int pathLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var dir = Direction2D.GetRandomDirection();
        var currentPosition = initialPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < pathLength; i++)
        {
            currentPosition += dir;
            corridor.Add(currentPosition);
        }
        return corridor;
    }
}

public static class Direction2D
{
    public static List<Vector2Int> directionEnums = new List<Vector2Int>
    {
        new Vector2Int(0,1), // north
        new Vector2Int(1,0), // east
        new Vector2Int(0,-1),// south
        new Vector2Int(-1,0),// west
    };

    public static Vector2Int GetRandomDirection()
    {
        return directionEnums[Random.Range(0, directionEnums.Count)];
    }
}
