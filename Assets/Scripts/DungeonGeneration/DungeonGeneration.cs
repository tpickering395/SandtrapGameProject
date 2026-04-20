using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGeneration : AbstractProceduralGenerator
{
    [SerializeField]
    protected SRWData externalParameters;
    protected override void StartProceduralGeneration()
    {
        HashSet<Vector2Int> floorCoords = GenerateRandomWalk(externalParameters, initialPos);
        mapVisualizer.Clear();
        mapVisualizer.PaintFloor(floorCoords);
        WallGenerator.CreateWalls(floorCoords, mapVisualizer);
    }

    protected HashSet<Vector2Int> GenerateRandomWalk(SRWData parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorCoords = new HashSet<Vector2Int>();

        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralDungeonGenerationAlgorithms.RandomWalk(currentPosition, parameters.pathLength);
            floorCoords.UnionWith(path);
            if (parameters.RandomizeInitIterations)
                currentPosition = floorCoords.ElementAt(Random.Range(0, floorCoords.Count));
        }
        return floorCoords;
    }
}
